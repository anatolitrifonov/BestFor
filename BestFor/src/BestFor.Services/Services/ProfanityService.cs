using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Services.Profanity;
using BestFor.Domain.Entities;
using BestFor.Data;

namespace BestFor.Services.Services
{
    public class ProfanityService: IProfanityService
    {
        private ICacheManager _cacheManager;
        private IRepository<BadWord> _repository;

        // private static 
        public ProfanityService(ICacheManager cacheManager, IRepository<BadWord> repository)
        {
            _cacheManager = cacheManager;
            _repository = repository;
        }

        private KeyDataSource<BadWord> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_BADWORDS_DATA);
            if (data == null)
            {
                var dataSource = new KeyDataSource<BadWord>();
                // Load all data
                dataSource.Initialize(_repository, 0);
                _cacheManager.Add(CacheConstants.CACHE_KEY_BADWORDS_DATA, dataSource);
                return dataSource;
            }
            return (KeyDataSource<BadWord>)data;
        }

        public ProfanityCheckResult CheckProfanity(string input)
        {
            var result = new ProfanityCheckResult();
            // Check blank first.
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) { result.NoData = true; return result; }
            // Check funny characters
            result.HasBadCharacters = !ProfanityFilter.AllCharactersAllowed(input);
            // Having bad character is enough to not touch cache
            if (result.HasBadCharacters) return result;

            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = GetCachedData();
            result.ProfanityWord = ProfanityFilter.GetProfanity(input, cachedData.Items);
            return result;
        }
    }
}
