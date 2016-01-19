using Newtonsoft.Json.Linq;
using BestFor.Dto;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface to search for resource strings by culture.
    /// </summary>
    public interface IResourcesService
    {
        /// <summary>
        /// Find the string by key for a given culture
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string culture, string key);

        /// <summary>
        /// Find a set of strings for set of keys
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        string[] GetStrings(string culture, string[] keys);

        /// <summary>
        /// Find strings for keys and return as javascript array.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="javaScriptVariableName"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        string GetStringsAsJavaScript(string culture, string javaScriptVariableName, string[] keys);

        JObject GetStringsAsJson(string culture, string[] keys);

        CommonStringsDto GetCommonStrings(string culture);
    }
}
