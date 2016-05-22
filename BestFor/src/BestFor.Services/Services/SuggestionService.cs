using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using Microsoft.Extensions.Logging;
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
    public class SuggestionService: ISuggestionService
    {
        private ICacheManager _cacheManager;
        private IRepository<Suggestion> _repository;
        private ILogger _logger;

        /// <summary>
        /// Cache and repository are injected in startup.
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="repository"></param>
        public SuggestionService(ICacheManager cacheManager, IRepository<Suggestion> repository, ILoggerFactory loggerFactory)
        {
            _cacheManager = cacheManager;
            _repository = repository;
            _logger = loggerFactory.CreateLogger<SuggestionService>();
            _logger.LogInformation("created SuggestionService");
        }

        /// <summary>
        /// Find suggestion in cache.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// Loads cache if empty
        /// </returns>
        public async Task<IEnumerable<SuggestionDto>> FindSuggestions(string input)
        {
            // We are only looking in cache.
            var cachedData = GetCachedData();
            return await Task.FromResult(cachedData.FindTopItems(input).Select(x => x.ToDto()));
        }

        /// <summary>
        /// Save suggestion to the database and to cache.
        /// </summary>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public async Task<Suggestion> AddSuggestion(SuggestionDto suggestion)
        {
            var suggestionObject = new Suggestion();
            suggestionObject.FromDto(suggestion);

            // Repository might get a different object back.
            // We will also let repository do the counting
            suggestionObject = await PersistSuggestion(suggestionObject);

            var cachedData = GetCachedData();
            cachedData.Insert(suggestionObject);

            return suggestionObject;
        }

        #region Private Methods
        /// <summary>
        /// Add suggestion to the database. Checks if exists first.
        /// </summary>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        private async Task<Suggestion> PersistSuggestion(Suggestion suggestion)
        {
            // Find if answer already exists
            var existingSuggestion = _repository.Queryable().Where(x => x.Phrase == suggestion.Phrase).FirstOrDefault();
            // Insert if new.
            if (existingSuggestion == null)
            {
                _repository.Insert(suggestion);
            }
            await _repository.SaveChangesAsync();
            return existingSuggestion == null ? suggestion : existingSuggestion;
        }


        /// <summary>
        /// Check cache first, if not there, load and save in cache.
        /// </summary>
        /// <returns></returns>
        private KeyDataSource<Suggestion> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA);
            if (data == null)
            {
                var dataSource = new KeyDataSource<Suggestion>();
                // 0 means load all
                dataSource.Initialize(_repository, 0);
                _cacheManager.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, dataSource);
                return dataSource;
            }
            return (KeyDataSource<Suggestion>)data;
        }
        #endregion
    }
}
