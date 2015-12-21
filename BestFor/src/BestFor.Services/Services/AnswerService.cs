using System.Threading;
using System.Threading.Tasks;
using System;
using BestFor.Dto;
using BestFor.Services.Services;
using BestFor.Services.DataSources;
using BestFor.Services.Cache;
using BestFor.Data;
using System.Collections.Generic;
using System.Linq;
using BestFor.Domain.Entities;

namespace BestFor.Services.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    /// <summary>
    /// Suggestion service implementation
    /// </summary>
    public class AnswerService : IAnswerService
    {
        private ICacheManager _cacheManager;
        private IRepository<Answer> _repository;

        // private static 
        public AnswerService(ICacheManager cacheManager, IRepository<Answer> repository)
        {
            _cacheManager = cacheManager;
            _repository = repository;
        }
        
        private KeyIndexedDataSource<Answer> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWERS_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<Answer>();
                dataSource.Initialize(_repository);
                _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWERS_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<Answer>)data;
        }

        public IEnumerable<AnswerDto> FindAnswers(string leftWord, string rightWord)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = cachedData.Find(Answer.FormKey(leftWord, rightWord));
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto());
        }

        public IEnumerable<AnswerDto> FindTopAnswers(string leftWord, string rightWord)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = cachedData.FindTopItems(Answer.FormKey(leftWord, rightWord));
            if (result == null) return Enumerable.Empty<AnswerDto>();
            return result.Select(x => x.ToDto());
        }

        public AnswerDto FindExact(string leftWord, string rightWord, string phrase)
        {
            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = GetCachedData();
            // This is just getting a list of answers with number of "votes" for each. Cache stored answers, not votes.
            // Each answer in cache has number of votes.
            var result = cachedData.FindExact(Answer.FormKey(leftWord, rightWord), phrase);
            if (result == null) return null;
            return result.ToDto();
        }

        public async Task<Answer> AddAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);

            // Repository might get a different object back.
            // We will also let repository do the counting
            answerObject = await PersistAnswer(answerObject);

            var cachedData = GetCachedData();
            cachedData.Insert(answerObject);

            return answerObject;
        }

        private async Task<Answer> PersistAnswer(Answer answer)
        {
            // Find if answer already exists
            var existingAnswer = _repository.List()
                .Where(x => x.LeftWord == answer.LeftWord && x.RightWord == answer.RightWord && x.Phrase == answer.Phrase)
                .FirstOrDefault();
            // Insert if new.
            if (existingAnswer == null)
            {
                answer.Count = 1;
                _repository.Insert(answer);
            }
            // Update if already there.
            else
            {
                existingAnswer.Count++;
                _repository.Update(existingAnswer);
            }
            await _repository.SaveChangesAsync();
            return existingAnswer == null ? answer : existingAnswer;
        }
    }
}
