using SFML.Graphics;
using SFML.Window;
using Luftschiff.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff {
    static class Initializer {
        public static void initialize() {
            initializeWindow();
            initializeMouse();
            initializeAssets();
            initializeMisc();
        }

        private static void initializeMouse() {
            Controller.Window.MouseButtonPressed += MouseHandler.click;
            Controller.Window.MouseWheelMoved += MouseHandler.scroll;
            Controller.Window.MouseButtonReleased += MouseHandler.release;
            Controller.Window.MouseMoved += MouseHandler.move;
        }

        private static void initializeWindow() {
            //initialize window
            Controller.Window = new RenderWindow(new VideoMode(1366, 768), "Luftschiff", Styles.Default);
            Controller.Window.SetVerticalSyncEnabled(true);
            Controller.Window.SetFramerateLimit(35);
            Controller.Window.Closed += delegate { Controller.Window.Close(); };
            
            //init view
            Controller.View = new View(new FloatRect(0, 0, Controller.Window.Size.X, Controller.Window.Size.Y));
            Controller.Window.SetView(Controller.View);
        }

        private static void initializeMisc() {

        }

        private static void initializeAssets() {
            //Globals.startMenuTextures.Add(new Texture("Assets/Menus/StartMenu/start.png"));
        }
    }
}
