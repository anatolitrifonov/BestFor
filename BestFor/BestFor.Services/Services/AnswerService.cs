using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Domain.Masks;
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
        #region Private Classes
        private struct PersistAnswerResult
        {
            public Answer Answer;
            public bool IsNew;
        }
        #endregion

        public const int TRENDING_TOP_TODAY = 9;
        public const int TRENDING_TOP_OVERALL = 9;
        public const int DEFAULT_SEARCH_RESULT_COUNT = 15;

        private ICacheManager _cacheManager;
        private IAnswerRepository _repository;
        private ILogger _logger;
        private IUserService _userService;

        // private static 
        public AnswerService(ICacheManager cacheManager, IAnswerRepository repository, ILoggerFactory loggerFactory, IUserService userService)
        {
            _cacheManager = cacheManager;
            _repository = repository;
            _userService = userService;
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

        public async Task<AddedAnswerDto> AddAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);

            // Repository might get a different object back.
            // We will also let repository do the counting. Repository increases the count.
            var persistResult = await PersistAnswer(answerObject);

            // Add to cache.
            var cachedData = await GetCachedData();
            await cachedData.Insert(persistResult.Answer);

            // Add to left cache
            var leftCachedData = await GetLeftCachedData();
            await leftCachedData.Insert(new AnswerLeftMask(persistResult.Answer));

            // Add to right cache
            var rightCachedData = await GetRightCachedData();
            await rightCachedData.Insert(new AnswerRightMask(persistResult.Answer));

            // Add to trending today
            AddToTrendingToday(persistResult.Answer);

            // Add to thrending overall
            AddToTrendingOverall(persistResult.Answer);

            // Update User if new answer
            if (persistResult.IsNew) await _userService.UpdateUserFromAnswer(answerObject);

            var result = new AddedAnswerDto() { Answer = persistResult.Answer.ToDto() };

            return result;
        }

        public async Task<AnswerDto> UpdateAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);

            // Repository might get a different object back.
            // We will also let repository do the counting. Repository increases the count.
            var persistResult = await PersistAnswer(answerObject);

            // Update object in cache.
            var cachedData = await GetCachedData();
            var cachedAnswer = await cachedData.FindExactById(answerObject.Id);
            // This is all we are updating for now.
            cachedAnswer.Category = answerObject.Category;

            return answer;
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

        public async Task<AnswerDto> FindByAnswerId(int answerId)
        {
            var cachedData = await GetCachedData();
            var answer = await cachedData.FindExactById(answerId);
            // Will be strange if not found ... but have to check.
            if (answer == null) return null;
            return answer.ToDto();
        }

        /// <summary>
        /// Find all answers for user going directly to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindDirectByUserId(string userId)
        {
            var data = _repository.FindByUserId(userId);
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }

        /// <summary>
        /// Find all answers with no user going directly to the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindDirectBlank()
        {
            var data = _repository.FindAnswersWithNoUser();
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }


        /// <summary>
        /// Hides answer is the database and removes it from cache.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public async Task<int> HideAnswer(int answerId)
        {
            if (answerId <= 0) return answerId;

            // Find if answer already exists
            var existingAnswer = _repository.Queryable().Where(x => x.Id == answerId).FirstOrDefault();
            // Return if does not.
            if (existingAnswer == null) return answerId;
            // Remove from cache
            var cachedData = await GetCachedData();
            await cachedData.Delete(existingAnswer);

            // Update repo
            existingAnswer.IsHidden = true;
            _repository.Update(existingAnswer);

            // Save changes.
            await _repository.SaveChangesAsync();

            return answerId;
        }

        /// <summary>
        /// Find the top N answers matching the left word
        /// </summary>
        /// <param name="leftWord"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindLeftAnswers(string leftWord)
        {
            return await FindLeftAnswers(leftWord, DEFAULT_SEARCH_RESULT_COUNT);
        }

        /// <summary>
        /// Find top <paramref name="count"/> answers matching the left word
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindLeftAnswers(string leftWord, int count)
        {
            if (string.IsNullOrEmpty(leftWord) || string.IsNullOrWhiteSpace(leftWord))
                throw new Exception("Invalid leftWord parameter passed to AnswerService.FindLeftAnswers");
            if (count < 1)
                throw new Exception("Invalid count parameter passed to AnswerService.FindLeftAnswers");

            var cachedData = await GetLeftCachedData();

            var result = await cachedData.Find(leftWord);

            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto()).Take(count);
        }

        /// <summary>
        /// Find the top N answers matching the right word
        /// </summary>
        /// <param name="rightWord"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindRightAnswers(string rightWord)
        {
            return await FindRightAnswers(rightWord, DEFAULT_SEARCH_RESULT_COUNT);
        }

        /// <summary>
        /// Find top <paramref name="count"/> answers matching the right word
        /// </summary>
        /// <param name="rightWord"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDto>> FindRightAnswers(string rightWord, int count)
        {
            if (string.IsNullOrEmpty(rightWord) || string.IsNullOrWhiteSpace(rightWord))
                throw new Exception("Invalid rightWord parameter passed to AnswerService.FindRightAnswers");
            if (count < 1)
                throw new Exception("Invalid count parameter passed to AnswerService.FindRightAnswers");

            var cachedData = await GetRightCachedData();

            var result = await cachedData.Find(rightWord);

            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto()).Take(count);
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

        private async Task<PersistAnswerResult> PersistAnswer(Answer answer)
        {
            // Find if answer already exists
            var existingAnswer = _repository.Queryable()
                .Where(x => x.LeftWord == answer.LeftWord && x.RightWord == answer.RightWord && x.Phrase == answer.Phrase)
                .FirstOrDefault();
            bool isNew = true;

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
                // Only category is updated
                existingAnswer.Category = answer.Category;
                _repository.Update(existingAnswer);
                isNew = false;
            }

            await _repository.SaveChangesAsync();

            // return new or not
            return new PersistAnswerResult() { Answer = existingAnswer, IsNew = isNew }; 
        }

        private async Task<KeyIndexedDataSource<Answer>> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWERS_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<Answer>();
                await dataSource.Initialize(_repository.Active());
                _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWERS_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<Answer>)data;
        }

        /// <summary>
        /// Get data indexed on the left word
        /// 
        /// Load from normally cached data if empty.
        /// </summary>
        /// <returns></returns>
        private async Task<KeyIndexedDataSource<AnswerLeftMask>> GetLeftCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_LEFT_ANSWERS_DATA);
            if (data == null)
            {
                // Initialize from answers
                var dataSource = await GetCachedData();
                var allItems = await dataSource.All();
                var leftDataSource = new KeyIndexedDataSource<AnswerLeftMask>();
                await leftDataSource.Initialize(allItems.Select(x => new AnswerLeftMask(x)));
                _cacheManager.Add(CacheConstants.CACHE_KEY_LEFT_ANSWERS_DATA, leftDataSource);
                return leftDataSource;
            }
            return (KeyIndexedDataSource<AnswerLeftMask>)data;
        }

        /// <summary>
        /// Get data indexed on the right word
        /// 
        /// Load from normally cached data if empty.
        /// </summary>
        /// <returns></returns>
        private async Task<KeyIndexedDataSource<AnswerRightMask>> GetRightCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_RIGHT_ANSWERS_DATA);
            if (data == null)
            {
                // Initialize from answers
                var dataSource = await GetCachedData();
                var allItems = await dataSource.All();
                var rightDataSource = new KeyIndexedDataSource<AnswerRightMask>();
                await rightDataSource.Initialize(allItems.Select(x => new AnswerRightMask(x)));
                _cacheManager.Add(CacheConstants.CACHE_KEY_RIGHT_ANSWERS_DATA, rightDataSource);
                return rightDataSource;
            }
            return (KeyIndexedDataSource<AnswerRightMask>)data;
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
