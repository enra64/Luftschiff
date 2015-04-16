using SFML.Graphics;
using SFML.Window;

using Luftschiff.Graphics.Lib;
using Luftschiff.Code;

namespace Luftschiff {
    static class Initializer {
        public static void Initialize() {
            InitializeWindow();
            InitializeMouse();
            InitializeAssets();
            InitializeMisc();
        }

        private static void InitializeMouse() {
            Controller.Window.MouseButtonPressed += MouseHandler.click;
            Controller.Window.MouseWheelMoved += MouseHandler.scroll;
            Controller.Window.MouseButtonReleased += MouseHandler.release;
            Controller.Window.MouseMoved += MouseHandler.move;
        }

        private static void InitializeWindow() {
            //initialize window
            Controller.Window = new RenderWindow(new VideoMode(1366, 768), "Luftschiff", Styles.Default);
            Controller.Window.SetVerticalSyncEnabled(true);
            Controller.Window.SetFramerateLimit(35);
            Controller.Window.Closed += delegate { Controller.Window.Close(); };
            
            //init view
            Controller.View = new View(new FloatRect(0, 0, Controller.Window.Size.X, Controller.Window.Size.Y));
            Controller.Window.SetView(Controller.View);
        }

        private static void InitializeMisc() {
            Globals.DialogFont = new Font("Assets/StandardFontSteamwreck.otf");
        }

        private static void InitializeAssets() {
            Globals.BackgroundTexture = new Texture("Assets/Graphics/testbg.png");
        }
    }
}
