using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using SFML.System;
using SFML.Window;

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

        public static Keyboard.Key TranslateIntegerToNumKey(int keyIndex)
        {
            switch (keyIndex)
            {
                case 0:
                    return Keyboard.Key.Num0;
                case 1:
                    return Keyboard.Key.Num1;
                case 2:
                    return Keyboard.Key.Num2;
                case 3:
                    return Keyboard.Key.Num3;
                case 4:
                    return Keyboard.Key.Num4;
                case 5:
                    return Keyboard.Key.Num5;
                case 6:
                    return Keyboard.Key.Num6;
                case 7:
                    return Keyboard.Key.Num7;
                case 8:
                    return Keyboard.Key.Num8;
                case 9:
                    return Keyboard.Key.Num9;
                default:
                    throw new IndexOutOfRangeException("Bad key index");
            }
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
