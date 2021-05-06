using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace exercise1.Extensions
{
    public static class StringExtensions
    {

       /// <summary>
       /// Split a string using the provided regex
       /// </summary>
       /// <param name="input">string to be split</param>
       /// <param name="regexPattern">the regex to be used to capture items of interest</param>
       /// <returns>an IEnmerable of matched strings</returns>
        public static IEnumerable<string> SplitByRegex(this string input, string regexPattern) {                         
            var matches = Regex.Matches(input, regexPattern);            
            return matches.Cast<Match>().Select(m => m.Groups[1].Value);
        }        
    }
}