using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Global
{
    class Util
    {
        public static String loadCrewText()
        {
            String res =System.IO.File.ReadAllText("Assets/Texts/a.txt");
            return res;
        }

    }
}
