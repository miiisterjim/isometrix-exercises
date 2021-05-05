using System;
using System.Collections.Generic;
using System.Linq;
using exercise1.Exceptions.StringCalculator;
using exercise1.Extensions;

namespace exercise1
{
    public static class StringCalculator
    {
        private static readonly string[] DEFAULT_DELIMITERS = new string[] { ",", Environment.NewLine };        
        private static readonly string DELIMITER_SETTING_INDICATOR = "//";

        public static int Add(string input, IEnumerable<Func<int,bool>> termFilters = null) {
            
            if(input.Equals(string.Empty)) {
                return 0;
            }

            int result =  input
                .ParseInput()
                .AssertValidTerms()
                .ApplyTermFilters(termFilters)
                .Sum();           

            return result;
        }

        private static IEnumerable<int> ParseInput(this string input) {

            string[] delimiters;
            var termStr = ExtractDelimiters(input, out delimiters);

            return termStr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Select(term => int.Parse(term));
        } 

        private static string ExtractDelimiters(string input, out string[] delimiters) {
            
            delimiters = DEFAULT_DELIMITERS;

            if(!input.StartsWith(DELIMITER_SETTING_INDICATOR))
                return input;

            var delimiterAndInputStr = RemoveDelimiterIndicator(input);
            
            var items = delimiterAndInputStr.Split(Environment.NewLine);
            delimiters = new string[] { items[0], Environment.NewLine };

            return string.Join(string.Empty, items.Skip(1));
        }

        private static string RemoveDelimiterIndicator(string input) {
            return input.Substring(2);
        }

        private static IEnumerable<int> ApplyTermFilters(this IEnumerable<int> terms, IEnumerable<Func<int,bool>> termFilters)
        {   
            if(termFilters == null)
                return terms;

            foreach(var filter in termFilters) {
                terms = terms = terms.Where(filter);
            }    

            return terms;        
        }        

        private static IEnumerable<int> AssertValidTerms(this IEnumerable<int> terms) {            
            
            if(terms.AllPositiveIntegers()) 
                return terms;

            var invalidTerms = terms.Where(x => x < 0);
            throw new InvalidTermsException(invalidTerms, "Negatives not allowed");            
        }
    }
}