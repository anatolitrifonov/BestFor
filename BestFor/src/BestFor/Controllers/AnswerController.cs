using System;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using BestFor.Services.Services;

namespace BestFor.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : BaseApiController
    {
        private const string QUERY_STRING_PARAMETER_LEFT_WORD = "leftWord";
        private const string QUERY_STRING_PARAMETER_RIGHT_WORD = "rightWord";
        private const int MINIMAL_WORD_LENGTH = 2;

        private IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<AnswerDto> Get()
        {
            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            if (!ParseAntiForgeryHeader()) return Enumerable.Empty<AnswerDto>();

            // validate input
            var leftWord = ValidateInputForGet(QUERY_STRING_PARAMETER_LEFT_WORD);
            if (leftWord == null) return Enumerable.Empty<AnswerDto>(); ;
            var rightWord = ValidateInputForGet(QUERY_STRING_PARAMETER_RIGHT_WORD);
            if (rightWord == null) return Enumerable.Empty<AnswerDto>(); ;

            // Thread.Sleep(4000);
            // call the service
            return _answerService.FindTopAnswers(leftWord, rightWord);
        }

        /// <summary>
        /// error indication will be no id assigned to the answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AnswerDto> AddAnswer(AnswerDto answer)
        {
            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            if (!ParseAntiForgeryHeader()) return answer;

            var addedAnswer = await _answerService.AddAnswer(answer);
            
            return addedAnswer.ToDto();
        }

        #region Private Members
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
            if (!Util.IsAlphaNumeric(userInput)) return null;
            return userInput;
        }
        #endregion Private Members
    }
}
