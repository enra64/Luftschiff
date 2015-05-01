using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Global.Utils {
    /// <summary>
    /// Helper Class to generate random numbers
    /// </summary>
    static class RandomHelper
    {
        /// <summary>
        /// static Random instance
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Returns true or false by random
        /// </summary>
        /// <param name="percentage">How probable true should be</param>
        /// <returns>Random true or false</returns>
        public static bool RandomTrue(int percentage)
        {
            return Random.Next(100) < percentage;
        }

        /// <summary>
        /// 50 percent chance
        /// </summary>
        /// <returns>true or false</returns>
        public static bool FiftyFifty()
        {
            return RandomTrue(50);
        }

        /// <summary>
        ///     Return a random int between null and the parameter minus one
        /// </summary>
        /// <param name="exclusiveMaximum">The exclusive maximum</param>
        /// <returns>Integer between 0 and exclusiveMaximum - 1</returns>
        public static int RandomUpTo(int exclusiveMaximum)
        {
            return Random.Next(exclusiveMaximum);
        }
    }
}
