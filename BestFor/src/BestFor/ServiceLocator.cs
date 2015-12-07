using BestFor.Services;
using BestFor.Services.Interfaces;

namespace BestFor
{
    /// <summary>
    /// Wraps around services injections.
    /// </summary>
    public class ServiceLocator
    {
        public static ISuggestionService GetSuggestionService()
        {
            return new SuggestionService();
        }
        public static IAnswerService GetAnswerService()
        {
            return new AnswerService();
        }
    }
}
