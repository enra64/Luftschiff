using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using SFML.System;

namespace Luftschiff.Code.Global
{
    class Util
    {
        public static String loadCrewText()
        {
            String res =System.IO.File.ReadAllText("Assets/Texts/a.txt");
            return res;
        }

        public static double  GetVector2fLength(Vector2f a)
        {
            return Math.Sqrt(a.X*a.X + a.Y*a.Y);
        }

        //dude you can literally write a - b
        public static Vector2f GetDiffVector2F(Vector2f a, Vector2f b)
        {
            return a - b;
        }

        /// <summary>
        /// Normalises a Vector
        /// </summary>
        /// <param name="input">Input vector</param>
        /// <returns>Normalised input vector</returns>
        public static Vector2f NormaliseVector(Vector2f input)
        {
            var vectorLength = (float) GetVector2fLength(input);
            return new Vector2f(input.X / vectorLength, input.Y / vectorLength);
        }

        public static double GetDistancebeweenVector2f(Vector2f a , Vector2f b)
        {
            return GetVector2fLength(GetDiffVector2F(a, b));
        }


    }
}
