using System.Collections.Generic;
using System.Linq;

namespace exercise1.Extensions
{
    public static class IntExtensions
    {

        /// <summary>
        /// Determine if all integers in a collection are greater than or equal to zero
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool AllPositiveIntegers(this IEnumerable<int> items) {
            return items.All(i => i >= 0);
        }        
    }
}