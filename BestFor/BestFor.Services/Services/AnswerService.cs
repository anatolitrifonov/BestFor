﻿using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    /// <summary>
    /// Suggestion service implementation
    /// </summary>
    public class AnswerService : IAnswerService
    {
        public const int TRENDING_TOP_TODAY = 9;
        public const int TRENDING_TOP_OVERALL = 9;

        private ICacheManager _cacheManager;
        private IAnswerRepository _repository;
        private ILogger _logger;

        // private static 
        public AnswerService(ICacheManager cacheManager, IAnswerRepository repository, ILoggerFactory loggerFactory)
        {
            _cacheManager = cacheManager;
            _repository = repository;
            _logger = loggerFactory.CreateLogger<AnswerService>();
            _logger.LogInformation("created AnswerService");
        }

        #region IAnswerService implementation
        public async Task<IEnumerable<AnswerDto>> FindAnswers(string leftWord, string rightWord)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = await GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = await cachedData.Find(Answer.FormKey(leftWord, rightWord));
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto());
        }

        public async Task<IEnumerable<AnswerDto>> FindTopAnswers(string leftWord, string rightWord)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = await GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = await cachedData.FindTopItems(Answer.FormKey(leftWord, rightWord));
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto());
        }

        public async Task<AnswerDto> FindExact(string leftWord, string rightWord, string phrase)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = await GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = await cachedData.FindExact(Answer.FormKey(leftWord, rightWord), phrase);
            if (result == null) return null;
            return result.ToDto();
        }

        public async Task<Answer> AddAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);

            // Repository might get a different object back.
            // We will also let repository do the counting. Repository increases the count.
            answerObject = await PersistAnswer(answerObject);

            // Add to cache.
            var cachedData = await GetCachedData();
            await cachedData.Insert(answerObject);

            // Add to trending today
            AddToTrendingToday(answerObject);

            // Add to thrending overall
            AddToTrendingOverall(answerObject);

            return answerObject;
        }

        public async Task<IEnumerable<AnswerDto>> FindAnswersTrendingToday()
        {
            var data = GetTodayTrendingCachedData();
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }

        public async Task<IEnumerable<AnswerDto>> FindAnswersTrendingOverall()
        {
            var data = GetOverallTrendingCachedData();
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }

        public async Task<AnswerDto> FindById(int answerId)
        {
            var cachedData = await GetCachedData();
            var answer = await cachedData.FindExactById(answerId);
            // Will be strange if not found ... but have to check.
            if (answer == null) return null;
            return answer.ToDto();
        }

        /// <summary>
        /// Hides answer is the database and removes it from cache.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public async Task HideAnswer(int answerId)
        {
            if (answerId <= 0) return;

            // Find if answer already exists
            var existingAnswer = _repository.Queryable().Where(x => x.Id == answerId).FirstOrDefault();
            // Return if does not.
            if (existingAnswer == null) return;
            // Remove from cache
            var cachedData = await GetCachedData();
            await cachedData.Delete(existingAnswer);

            // Update repo
            existingAnswer.IsHidden = true;
            _repository.Update(existingAnswer);

            // Save changes.
            await _repository.SaveChangesAsync();

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add answer to the list of answers trending today
        /// </summary>
        /// <param name="answer"></param>
        private void AddToTrendingToday(Answer answer)
        {
            // Currently assumes anser.Count = 1
            // Get today's trend
            var todaysTrending = GetTodayTrendingCachedData();
            // I doubt this will ever happen but lets just return for now. It should be empty in the worst case.
            if (todaysTrending == null) return;
            // Check if the last one has count = 1. Throw it away.
            var length = todaysTrending.Count;
            // Add at the end if we can. This means initially we loaded less than constant.
            if (length < TRENDING_TOP_TODAY)
            {
                todaysTrending.Add(answer);
                return;
            }
            // If last one is not with count 1 -> nothing to do. That means the last one already has more than one vote.
            if (todaysTrending[length - 1].Count > 1) return;
            // Logically that means the last one is with count one.
            // And logically we also need to check the date added.
            // But we are too lazy. It is only a few items. Let's just throw away the last one.
            todaysTrending.RemoveAt(length - 1);
            // I'd assume this adds at the start.
            todaysTrending.Insert(0, answer);
        }

        /// <summary>
        /// Add answer to the list of answers trending overall
        /// </summary>
        /// <param name="answer"></param>
        private void AddToTrendingOverall(Answer answer)
        {
            // Currently assumes anser.Count = 1
            // Get today's trend
            var overallTrending = GetOverallTrendingCachedData();
            // I doubt this will ever happen but lets just return for now. It should be empty in the worst case.
            if (overallTrending == null) return;
            // Check if the last one has count = 1. Throw it away.
            var length = overallTrending.Count;
            // Only add at the end if we can. This means initially we loaded less than constant.
            // We are not going to do any other calculations.
            if (length < TRENDING_TOP_OVERALL)
            {
                overallTrending.Add(answer);
                return;
            }
        }

        private async Task<Answer> PersistAnswer(Answer answer)
        {
            // Find if answer already exists
            var existingAnswer = _repository.Queryable()
                .Where(x => x.LeftWord == answer.LeftWord && x.RightWord == answer.RightWord && x.Phrase == answer.Phrase)
                .FirstOrDefault();
            // Insert if new.
            if (existingAnswer == null)
            {
                existingAnswer = answer;
                existingAnswer.Count = 1;
                _repository.Insert(existingAnswer);
            }
            // Update if already there.
            else
            {
                existingAnswer.Count++;
                _repository.Update(existingAnswer);
            }

            await _repository.SaveChangesAsync();

            // return existingAnswer == null ? answer : existingAnswer;
            return existingAnswer; 
        }

        private async Task<KeyIndexedDataSource<Answer>> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWERS_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<Answer>();
                await dataSource.Initialize(_repository);
                _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWERS_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<Answer>)data;
        }

        /// <summary>
        /// Get trending today data from cache. Initialize if needed.
        /// </summary>
        /// <returns></returns>
        private List<Answer> GetTodayTrendingCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_TRENDING_TODAY_DATA);
            if (data == null)
            {
                var trendingToday = _repository.FindAnswersTrendingToday(TRENDING_TOP_TODAY, DateTime.Now).ToList<Answer>();
                // If not enough get the rest from Overall Totals
                if (trendingToday.Count < TRENDING_TOP_TODAY)
                {
                    var trendingOverall = GetOverallTrendingCachedData();
                    var trendingTodayCount = trendingToday.Count;
                    // we are going to skip the ones that are already there
                    var added = 0;
                    for (var i = 0; added < TRENDING_TOP_TODAY - trendingTodayCount && i < trendingOverall.Count; i++)
                    {
                        // is it already there?
                        if (trendingToday.Count(x => x.Id == trendingOverall[i].Id) == 0)
                        {
                            trendingToday.Add(trendingOverall[i]);
                            added++;
                        }
                    }
                }
                _cacheManager.Add(CacheConstants.CACHE_KEY_TRENDING_TODAY_DATA, trendingToday);
                return trendingToday;
            }
            return (List<Answer>)data;
        }

        /// <summary>
        /// Get trending overall data from cache. Initialize if needed.
        /// </summary>
        /// <returns></returns>
        private List<Answer> GetOverallTrendingCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_TRENDING_OVERALL_DATA);
            if (data == null)
            {
                var trendingOverall = _repository.FindAnswersTrendingOverall(TRENDING_TOP_OVERALL).ToList<Answer>();
                _cacheManager.Add(CacheConstants.CACHE_KEY_TRENDING_OVERALL_DATA, trendingOverall);
                return trendingOverall;
            }
            return (List<Answer>)data;
        }
        #endregion
    }
}