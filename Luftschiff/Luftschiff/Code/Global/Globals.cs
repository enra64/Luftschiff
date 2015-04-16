﻿using SFML.Graphics;
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

        public static Font DialogFont;

        //colors and height for any dialog buttons
        public static int TWO_BUTTON_DIALOG_BUTTON_HEIGHT = 40;
        public static Color DIALOG_BUTTON_COLOR_NORMAL = Color.White;
        public static Color DIALOG_BUTTON_COLOR_HOVER = Color.Yellow;

        //globally used font sizes for text
        public static int FONT_SIZE_STANDARD = 20;
        public static int FONT_SIZE_HEADER = 40;
        public static int FONT_SIZE_SUBTEXT = 15;
    }
}
