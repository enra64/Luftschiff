using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code {
    /// <summary>
    /// A standard globals class for things like enums or textures that need
    /// to be accessible from everywhere.
    /// </summary>
    static class Globals {
        //static variables
        public enum EStateSelection
        {
            main,
            sub
        }

        public enum EGameStates
        {
            game
        }
    }
}
