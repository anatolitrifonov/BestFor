using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BestFor.Dto;
using System.Collections.Generic;

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
        Task<string> GetString(string culture, string key);

        /// <summary>
        /// Find a set of strings for set of keys
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<string[]> GetStrings(string culture, string[] keys);

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
        Task<string> GetStringsAsJavaScript(string culture, string javaScriptVariableName, string[] keys);

        /// <summary>
        /// Return dynamic json object containing keys and strings as properties and values.
        /// This object can be then rendered as jSon object on the client side.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<JObject> GetStringsAsJson(string culture, string[] keys);

        /// <summary>
        /// Return common strings for a given culture.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        Task<CommonStringsDto> GetCommonStrings(string culture);

        /// <summary>
        /// Load all known common strings.
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, CommonStringsDto>> GetCommonStringsForAllCultures();
    }
}
