using BestFor.Dto;
using BestFor.Services.Profanity;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
    [Route("api/[controller]")]
    public class SuggestionController : BaseApiController
    {
        /// <summary>
        /// Makes working with validation more comfortable
        /// </summary>
        private class ValidationResult
        {
            public bool Passed { get; set; } = true;

            public string ErrorMessage { get; set; }

            public string CleanedInput { get; set; }
        }

        private const string QUERY_STRING_PARAMETER_USER_INPUT = "userInput";
        private const int MINIMAL_WORD_LENGTH = 2;

        private ISuggestionService _suggestionService;
        private IProfanityService _profanityService;

        public SuggestionController(ISuggestionService suggestionService, IProfanityService profanityService)
        {
            _suggestionService = suggestionService;
            _profanityService = profanityService;
        }

        // GET: api/values
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<SuggestionsDto> Get()
        {
            var result = new SuggestionsDto();
            if (!ParseAntiForgeryHeader()) return result;

            // validate input
            var validationResult = ValidateInputForGet();
            if (!validationResult.Passed)
            {
                result.ErrorMessage = validationResult.ErrorMessage;
                return result;
            }
            // get and call the service
            //Thread.Sleep(4000);

            result.Suggestions = await _suggestionService.FindSuggestions(validationResult.CleanedInput);

            return result;
        }

        /// <summary>
        /// Returns validated query string for GET method or null.
        /// </summary>
        /// <returns></returns>
        private ValidationResult ValidateInputForGet()
        {
            // do not return anything on empty input
            if (!Request.Query.ContainsKey(QUERY_STRING_PARAMETER_USER_INPUT)) return null;
            var userInput = Request.Query[QUERY_STRING_PARAMETER_USER_INPUT][0];
            // Check null or spaces or empty
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput)) return null;
            userInput = userInput.Trim();
            // Check minimal length
            if (userInput.Length < MINIMAL_WORD_LENGTH + 1) return null;
            // This is what we will return
            var result = new ValidationResult() { CleanedInput = userInput };
            // Check for bad characters.
            string badCharacter = ProfanityFilter.FirstDisallowedCharacter(result.CleanedInput);
            // let's only serve alphanumeric for now.
            if (badCharacter != null)
            {
                result.Passed = false;
                result.ErrorMessage = badCharacter;
            }

            // if (ProfanityFilter.AllCharactersAllowed(userInput)) return userInput;
            // if (!ProfanityFilter.IsAlphaNumeric(userInput)) return null;
            // return null;
            return result;
        }
    }
}
