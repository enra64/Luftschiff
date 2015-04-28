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
    }
}
