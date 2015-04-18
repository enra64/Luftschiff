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

        public static Vector2f GetDiffVector2F(Vector2f a, Vector2f b)
        {
            return a - b;
        }

        public static double GetDistancebeweenVector2f(Vector2f a , Vector2f b)
        {
            return GetVector2fLength(GetDiffVector2F(a, b));
        }

    }
}
