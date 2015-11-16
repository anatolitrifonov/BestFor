using System;
using System.Text.RegularExpressions;

namespace BestFor
{
    /// <summary>
    /// All sorts of helper functions
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Validate that string is alphnumeric
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }
    }
}
