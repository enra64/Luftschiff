using Luftschiff.Code.Game;
using Luftschiff.Code.Game.AreavRooms;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;


namespace Luftschiff.Code {
    /// <summary>
    /// A standard globals class for things like enums or textures that need
    /// to be accessible from everywhere.
    /// </summary>
    static class Globals {
        //static variables
        /// <summary>
        /// Contains possible gamestates
        /// </summary>
        public enum EStates
        {
            menu,
            game,
            graphicstest
        }


        /// <summary>
        /// current standard font for use in dialogs
        /// </summary>
        public static Font DialogFont;

        /// <summary>
        /// The standard height for every dialog button
        /// </summary>
        public static int TWO_BUTTON_DIALOG_BUTTON_HEIGHT = 40;

        /// <summary>
        /// the standard color a dialog button uses when not hovered over
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_NORMAL = Color.White;

        /// <summary>
        /// standard color for hover
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_HOVER = Color.Yellow;

        /// <summary>
        /// when the button needs attention - originally for turn button
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_ATTENTIONSEEKER = Color.Red;



        //globally used font sizes for text
        public static int FONT_SIZE_STANDARD = 20;
        public static int FONT_SIZE_HEADER = 40;
        public static int FONT_SIZE_SUBTEXT = 15;

        public static Time FRAME_TIME;

        /// <summary>
        /// Texture for the background
        /// </summary>
        public static Texture BackgroundTexture;
        public static Texture DragonTexture;
        public static Texture GunTexture;

        public static Texture[] TileTextures;

        public static TurnHandler TurnHandler;
        public static Area AreaReference;
        public static Texture CrewTexture;
        public static Texture CannonBallTexture;
        public static SoundBuffer CannonSound;
    }
}
