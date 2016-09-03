using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Services.Profanity;
using System;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public class ProfanityService: IProfanityService
    {
        private ICacheManager _cacheManager;
        private IRepository<BadWord> _repository;
        private readonly IResourcesService _resourcesService;

        // private static 
        public ProfanityService(ICacheManager cacheManager, IRepository<BadWord> repository, IResourcesService resourcesService)
        {
            _cacheManager = cacheManager;
            _repository = repository;
            _resourcesService = resourcesService;
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

        public async Task<ProfanityCheckResult> CheckProfanity(string input)
        {
            return await CheckProfanity(input, null);
        }

        public async Task<ProfanityCheckResult> CheckProfanity(string input, string culture)
        {
            var result = new ProfanityCheckResult();
            // Check blank first.
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) { result.NoData = true; return result; }
            // Check funny characters
            result.HasBadCharacters = !ProfanityFilter.AllCharactersAllowed(input);
            // Having bad character is enough to not touch cache
            if (result.HasBadCharacters) return await LocalizeResult(result, culture);

            // Theoretically this shold never throw exception unless we got some timeout on initialization or something strange.
            var cachedData = GetCachedData();
            result.ProfanityWord = ProfanityFilter.GetProfanity(input, cachedData.Items);

            return await (result.HasIssues ? LocalizeResult(result, culture) : Task.FromResult<ProfanityCheckResult>(result));
        }

        /// <summary>
        /// Sets the localized error message in profanity check result object
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ProfanityCheckResult> LocalizeResult(ProfanityCheckResult result, string culture)
        {
            if (result == null)
                throw new Exception("Null result passed to ProfanityService.LocalizeResult");
            if (culture == null)
            {
                result.ErrorMessage = result.DefaultErrorMessage;
                return result;
            }
            if (result.HasBadCharacters)
            {
                // leave variable assignment in case we need to step through.
                var badCharacters = await _resourcesService.GetString(culture, Lines.ERROR_BAD_CHARACTERS);
                result.ErrorMessage = badCharacters;
            }
            if (result.ProfanityWord != null)
            {
                // leave variable assignment in case we need to step through.
                var errorProfanity = await _resourcesService.GetString(culture, Lines.ERROR_PROFANITY);
                result.ErrorMessage = (result.HasBadCharacters ? " " : string.Empty) + errorProfanity + ": " + result.ProfanityWord;
            }
            return result;
        }
    }
}
