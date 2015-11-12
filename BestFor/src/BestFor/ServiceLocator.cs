using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Services;

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
    }
}
