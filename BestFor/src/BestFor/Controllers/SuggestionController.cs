using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using BestFor.Dto;
using BestFor.Services.Service;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
    [Route("api/[controller]")]
    public class SuggestionController : Controller
    {
        private const string QUERY_STRING_PARAMETER_USER_INPUT = "userInput";
        private const int MINIMAL_WORD_LENGTH = 2;

        private ISuggestionService _suggestionService;
        public SuggestionController(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<SuggestionDto> Get()
        {
            // validate input
            var userInput = ValidateInputForGet();
            if (userInput == null) return null;
            // get and call the service
            //Thread.Sleep(4000);

            return _suggestionService.FindSuggestions(userInput);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Returns validated query string for GET method or null.
        /// </summary>
        /// <returns></returns>
        private string ValidateInputForGet()
        {
            // do not return anything on empty input
            if (!Request.Query.ContainsKey(QUERY_STRING_PARAMETER_USER_INPUT)) return null;
            var userInput = Request.Query[QUERY_STRING_PARAMETER_USER_INPUT][0];
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
