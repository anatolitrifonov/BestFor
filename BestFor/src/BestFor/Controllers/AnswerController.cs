using System;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using BestFor.Services.Services;
using BestFor.Services.Profanity;

namespace BestFor.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : BaseApiController
    {
        private const string QUERY_STRING_PARAMETER_LEFT_WORD = "leftWord";
        private const string QUERY_STRING_PARAMETER_RIGHT_WORD = "rightWord";
        private const int MINIMAL_WORD_LENGTH = 2;

        private IAnswerService _answerService;
        private IProfanityService _profanityService;
        private ISuggestionService _suggestionService;

        public AnswerController(IAnswerService answerService, IProfanityService profanityService, ISuggestionService suggestionService)
        {
            _answerService = answerService;
            _profanityService = profanityService;
            _suggestionService = suggestionService;
        }

        // GET: api/values
        [HttpGet]
        public AnswersDto Get()
        {
            var result = new AnswersDto();

            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            if (!ParseAntiForgeryHeader()) return result;

            // validate input
            var leftWord = ValidateInputForGet(QUERY_STRING_PARAMETER_LEFT_WORD);
            if (leftWord == null) return result;
            var rightWord = ValidateInputForGet(QUERY_STRING_PARAMETER_RIGHT_WORD);
            if (rightWord == null) return result;

            // Thread.Sleep(4000);
            // call the service
            result.Answers = _answerService.FindTopAnswers(leftWord, rightWord);

            return result;
        }

        /// <summary>
        /// error indication will be no id assigned to the answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AddedAnswerDto> AddAnswer(AnswerDto answer)
        {
            var result = new AddedAnswerDto() { Answer = answer };
            // Basic checks first
            if (result.Answer == null) return SetErrorMessage(result, "Incoming data is null.");

            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            if (!ParseAntiForgeryHeader()) return SetErrorMessage(result, "Antiforgery issue");

            // Let's first check for profanities.
            bool gotProfanityIssues = CheckProfanity(_profanityService, result);
            if (gotProfanityIssues) return result;

            // Add left word and right word to suggestions
            var addedSuggestion = await _suggestionService.AddSuggestion(new SuggestionDto() { Phrase = answer.LeftWord });
            addedSuggestion = await _suggestionService.AddSuggestion(new SuggestionDto() { Phrase = answer.RightWord });

            // Add answer
            var addedAnswer = await _answerService.AddAnswer(answer);
            result.Answer = addedAnswer.ToDto();
            return result;
        }

        #region Private Members
        private AddedAnswerDto SetErrorMessage(AddedAnswerDto answer, string errorMessage)
        {
            answer.ErrorMessage = errorMessage;
            return answer;
        }

        /// <summary>
        /// Check answer data received from user for profanity.
        /// Set the error message in the answer object if anything found.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="answer"></param>
        /// <returns>true if profanity found</returns>
        private bool CheckProfanity(IProfanityService service, AddedAnswerDto answer)
        {
            var profanityCheckResult = service.CheckProfanity(answer.Answer.LeftWord);
            if (profanityCheckResult.HasIssues)
            {
                answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                return true;
            }
            profanityCheckResult = service.CheckProfanity(answer.Answer.RightWord);
            if (profanityCheckResult.HasIssues)
            {
                answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                return true;
            }
            profanityCheckResult = service.CheckProfanity(answer.Answer.Phrase);
            if (profanityCheckResult.HasIssues)
            {
                answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns validated query string for GET method or null.
        /// </summary>
        /// <returns></returns>
        private string ValidateInputForGet(string parameterName)
        {
            // do not return anything on empty input
            if (!Request.Query.ContainsKey(parameterName)) return null;
            var userInput = Request.Query[parameterName][0];
            // Check null or spaces or empty
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput)) return null;
            userInput = userInput.Trim();
            // Check minimal length
            if (userInput.Length < MINIMAL_WORD_LENGTH + 1) return null;
            // let's only serve alphanumeric for now.
            if (ProfanityFilter.AllCharactersAllowed(userInput))
                return userInput;
            // if (!ProfanityFilter.IsAlphaNumeric(userInput)) return null;
            return null;
        }
        #endregion Private Members
    }
}
