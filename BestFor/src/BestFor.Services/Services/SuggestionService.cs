using BestFor.Dto;
using System.Collections.Generic;
using BestFor.Services.DataSources;
using System.Linq;
using BestFor.Domain.Entities;
using System.Threading.Tasks;
using BestFor.Services.Cache;
using BestFor.Data;

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
        // private static 
        public SuggestionService(ICacheManager cacheManager, IRepository<Suggestion> repository)
        {
            _cacheManager = cacheManager;
            _repository = repository;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<SuggestionDto> FindSuggestions(string input)
        {
            var cachedData = GetCachedData();
            return cachedData.FindTopItems(input).Select(x => x.ToDto());
        }

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
    }
}
