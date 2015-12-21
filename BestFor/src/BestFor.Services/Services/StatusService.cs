using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Data;

namespace BestFor.Services.Services
{
    public class StatusService : IStatusService
    {
        private ICacheManager _cacheManager;
        private IRepository<Answer> _answersRepository;
        private IRepository<Suggestion> _suggestionRepository;

        public StatusService(ICacheManager cacheManager, IRepository<Answer> answersRepository, IRepository<Suggestion> suggestionRepository)
        {
            _cacheManager = cacheManager;
            _answersRepository = answersRepository;
            _suggestionRepository = suggestionRepository;
        }

        public SystemStateDto GetSystemStatus()
        {
            var result = new SystemStateDto();

            var answersData = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWERS_DATA);
            if (answersData == null)
            {
                result.AnswersCacheStatus = "Not loaded";
            }
            else
            {
                result.AnswersCacheStatus = "Loaded";
                result.AnswersCacheNumberItems = ((KeyIndexedDataSource<Answer>)answersData).Size;
            }

            var suggestionsData = _cacheManager.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA);
            if (suggestionsData == null)
            {
                result.SuggestionsCacheStatus = "Not loaded";
            }
            else
            {
                result.SuggestionsCacheStatus = "Loaded";
                result.SuggestionsCacheNumberItems = ((KeyDataSource<Suggestion>)suggestionsData).Size;
            }

            return result;
        }

        public int InitAnswers()
        {
            var dataSource = new KeyIndexedDataSource<Answer>();
            dataSource.Initialize(_answersRepository);
            _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWERS_DATA, dataSource);
            return dataSource.Size;
        }

        public int InitSuggestions()
        {
            var dataSource = new KeyDataSource<Suggestion>();
            dataSource.Initialize(_suggestionRepository, 10000);
            _cacheManager.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, dataSource);
            return dataSource.Size;
        }
    }
}
