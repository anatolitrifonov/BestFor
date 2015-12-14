using System;
using BestFor.Dto;
using BestFor.Services.Interfaces;
using BestFor.Services.DatSources;
using BestFor.Services.Cache;
using BestFor.Data;
using System.Collections.Generic;
using System.Linq;
using BestFor.Domain.Entities;

namespace BestFor.Services
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
            object data = _cacheManager.Get("Answers Cache");
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<Answer>();
                dataSource.Initialize(_repository);
                _cacheManager.Add("Answers Cache", dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<Answer>)data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<AnswerDto> FindAnswers(string leftWord, string rightWord)
        {
            var cachedData = GetCachedData();
            // cachedData.

            // Find data source for suggestions
            // Call its find suggestions method
            // Ask results to convert itselft to Dto 
            return DataLocator.GetAnswersDataSource().FindAnswers(leftWord, rightWord).Select(x => x.ToDto());
        }

        public Answer AddAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);
            return DataLocator.GetAnswersDataSource().AddAnswer(answerObject);
        }

    }
}
