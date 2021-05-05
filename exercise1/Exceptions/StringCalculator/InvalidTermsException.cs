using System.Collections.Generic;

namespace exercise1.Exceptions.StringCalculator
{
    /// <summary>
    /// Exception describing invalid terms provided to the StringCalculator
    /// </summary>
    public class InvalidTermsException : System.Exception
    {
        private readonly IEnumerable<int> invalidTerms;
        private readonly string reason = "Invalid terms provided";
    
        /// <summary>
        /// Create a InvalidTermsException with a generic error message that identifies the invalid terms
        /// </summary>
        /// <param name="invalidTerms">the terms that were invalid</param>
        public InvalidTermsException(IEnumerable<int> invalidTerms) {
            this.invalidTerms = invalidTerms;
        }

        /// <summary>
        /// Create a InvalidTermsException with a specified error message that identifies the invalid terms
        /// </summary>
        /// <param name="invalidTerms"></param>
        /// <param name="reason"></param>
        public InvalidTermsException(IEnumerable<int> invalidTerms, string reason) {
            this.invalidTerms = invalidTerms;

            if(!reason.Equals(string.Empty)) {
                this.reason = reason;
            }                
        }

        public override string ToString()
        {
            return $"{reason} - {string.Join(",", invalidTerms)}";
        }




        
    
    
        
    }
}