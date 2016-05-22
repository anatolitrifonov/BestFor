using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BestFor.Services.Services
{
    /// <summary>
    /// Simple service that returns language specific strings by key.
    /// </summary>
    /// <remarks>
    /// Stores data in cache as list. For now we think we can get away with list because we are not planning to have more than 5k strings.
    /// We think we can go through this using linq just fine.
    /// Assumes that an instance is created per request and culture for a given instance is always the same.
    /// </remarks>
    public class ResourcesService : IResourcesService
    {
        /// <summary>
        /// Store services for between the calls.
        /// </summary>
        private ICacheManager _cacheManager;
        private IRepository<ResourceString> _repository;
        private const string DEFAULT_CULTURE = "en-US";
        /// <summary>
        /// Save data between calls. This object might be needed several times. No need to go to cache every time.
        /// </summary>
        private CommonStringsDto _commonStrings;

        public ResourcesService(ICacheManager cacheManager, IRepository<ResourceString> repository)
        {
            _cacheManager = cacheManager;
            _repository = repository;
        }

        /// <summary>
        /// Return common strings for a given culture.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <remarks>Has to be a function. I'd rather not set the culture per instance.</remarks>
        public async Task<CommonStringsDto> GetCommonStrings(string culture)
        {
            // See if we got the strings already
            if (_commonStrings != null) return _commonStrings;
            // Well ... nothing we can do ... load from cache.
            var resourceStrings = GetCachedData();
            // Setup common strings so that we do not have to touch cache with no need.
            _commonStrings = LoadCommonStrings(culture, resourceStrings);
            return await Task.FromResult(_commonStrings);
        }

        /// <summary>
        /// Get the string for a given key in a given culture.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// If string for culture found return.
        /// If not found seach English
        /// If not found return key
        /// </remarks>
        public async Task<string> GetString(string culture, string key)
        {
            // Do some checks before we go to cache.
            // No need to touch cache if blanks.
            if (string.IsNullOrEmpty(culture) || string.IsNullOrWhiteSpace(culture)) return key;
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key)) return key;
            // Ok get cached strings
            var resourceStrings = GetCachedData();
            // Setup common strings so that we do not have to touch cache with no need.
            if (_commonStrings == null) _commonStrings = LoadCommonStrings(culture, resourceStrings);
            // Get the string for this culture
            return await Task.FromResult(FindOneString(culture, key, resourceStrings));
        }

        /// <summary>
        /// Find a set of strings for set of keys
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<string[]> GetStrings(string culture, string[] keys)
        {
            // Do some checks before we go to cache.
            // No need to touch cache if blanks.
            if (string.IsNullOrEmpty(culture) || string.IsNullOrWhiteSpace(culture)) return keys;
            if (keys == null) return keys;
            var result = new string[keys.Length];
            // Ok get cached strings
            var resourceStrings = GetCachedData();
            // Setup common strings so that we do not have to touch cache with no need.
            if (_commonStrings == null) _commonStrings = LoadCommonStrings(culture, resourceStrings);
            // Loop through the resources 
            for (var i = 0; i < keys.Length; i++)
            {
                if (string.IsNullOrEmpty(keys[i]) || string.IsNullOrWhiteSpace(keys[i]))
                    result[i] = keys[i];
                else
                    result[i] = FindOneString(culture, keys[i], resourceStrings);
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Find strings for keys and return as javascript json object.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="javaScriptVariableName"></param>
        /// <param name="keys"></param>
        /// <returns>
        /// script
        /// var javaScriptVariableName {
        /// "a" : "s",
        /// "b" : "v"
        /// }
        /// script
        /// </returns>
        public async Task<string> GetStringsAsJavaScript(string culture, string javaScriptVariableName, string[] keys)
        {
            var strings = await GetStrings(culture, keys);
            var sb = new StringBuilder("<script>\n\r")
                .Append("var ").Append(javaScriptVariableName).Append(" = {\n\r");
            for (var i = 0; i < keys.Length; i++)
            {
                // We need to check for doublequotes in values
                sb.Append("\"").Append(keys[i]).Append("\" : \"").Append(strings[i]).Append(i < keys.Length - 1 ? "\",\n\r" : "\"\n\r");
            }
            sb.Append("}\r\n</script>\n\r");
            return await Task.FromResult(sb.ToString());
        }

        /// <summary>
        /// Return dynamic json object containing keys and strings as properties and values.
        /// This object can be then rendered as jSon object on the client side.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<JObject> GetStringsAsJson(string culture, string[] keys)
        {
            // Get strings -> build json object
            var strings = await GetStrings(culture, keys);
            var result = new JObject();
            for (var i = 0; i < keys.Length; i++)
                result.Add(new JProperty(keys[i], strings[i]));
            return result;
        }

        /// <summary>
        /// Load all known common strings.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, CommonStringsDto>> GetCommonStringsForAllCultures()
        {
            var data = GetCachedData().Where(x => x.Key == "best_start_capital" || x.Key == "for_lower" || x.Key == "is_lower").ToList();
            var result = new Dictionary<string, CommonStringsDto>();
            var cultures = data.Select(x => x.CultureName).Distinct();
            foreach (var culture in cultures)
                result.Add(culture, LoadCommonStrings(culture, data));
            return await Task.FromResult(result);
        }


        #region Private Methods
        /// <summary>
        /// Find a string by key and culture in the list of resource strings
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="resourceStrings"></param>
        /// <returns></returns>
        private string FindOneString(string culture, string key, List<ResourceString> resourceStrings)
        {
            var getStringResult = resourceStrings.FirstOrDefault(x => x.CultureName == culture && x.Key == key);
            if (getStringResult != null) return getStringResult.Value;
            // Not found, get default
            getStringResult = resourceStrings.FirstOrDefault(x => x.CultureName == DEFAULT_CULTURE && x.Key == key);
            if (getStringResult != null) return getStringResult.Value;
            // Not found, return key
            return key;
        }

        /// <summary>
        /// Get data from cache. Load into cache from repo if needed.
        /// </summary>
        /// <returns></returns>
        private List<ResourceString> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_RESOURCES_DATA);
            if (data == null)
            {
                // Load all.
                List<ResourceString> resourceStrings = _repository.List().ToList();
                // Save
                _cacheManager.Add(CacheConstants.CACHE_KEY_RESOURCES_DATA, resourceStrings);
                return resourceStrings;
            }
            return (List<ResourceString>)data;
        }

        private CommonStringsDto LoadCommonStrings(string culture, List<ResourceString> resourceStrings)
        {
            var result = new CommonStringsDto();
            result.Best = FindOneString(culture, "best_start_capital", resourceStrings);
            result.For = FindOneString(culture, "for_lower", resourceStrings);
            result.Is = FindOneString(culture, "is_lower", resourceStrings);
            result.FlagLower = FindOneString(culture, "flag_lower", resourceStrings);
            result.FlagUpper = FindOneString(culture, "flag_upper", resourceStrings);
            result.VoteLower = FindOneString(culture, "vote_lower", resourceStrings);
            result.VoteUpper = FindOneString(culture, "vote_upper", resourceStrings);
            result.DescribeLower = FindOneString(culture, "describe_lower", resourceStrings);
            result.DescribeUpper = FindOneString(culture, "describe_upper", resourceStrings);
            result.MoreLower = FindOneString(culture, "more_lower", resourceStrings);
            result.MoreUpper = FindOneString(culture, "more_upper", resourceStrings);
            return result;
        }
        #endregion
    }
}
