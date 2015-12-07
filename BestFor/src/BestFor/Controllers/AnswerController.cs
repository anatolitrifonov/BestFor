using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private const string QUERY_STRING_PARAMETER_LEFT_WORD = "leftWord";
        private const string QUERY_STRING_PARAMETER_RIGHT_WORD = "rightWord";
        private const int MINIMAL_WORD_LENGTH = 2;

        // GET: api/values
        [HttpGet]
        public IEnumerable<AnswerDto> Get()
        {
            // validate input
            var leftWord = ValidateInputForGet(QUERY_STRING_PARAMETER_LEFT_WORD);
            if (leftWord == null) return null;
            var rightWord = ValidateInputForGet(QUERY_STRING_PARAMETER_RIGHT_WORD);
            if (rightWord == null) return null;
            // get and call the service
            //Thread.Sleep(4000);

            return ServiceLocator.GetAnswerService().FindAnswers(leftWord, rightWord);
        }

        [HttpPost]
        public int AddAnswer(AnswerDto answer)
        {
            return ServiceLocator.GetAnswerService().FindAnswers(leftWord, rightWord);
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
            if (!Util.IsAlphaNumeric(userInput)) return null;
            return userInput;
        }
    }
}
