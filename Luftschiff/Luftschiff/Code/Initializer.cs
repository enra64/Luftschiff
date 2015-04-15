using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter {
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
            Controller.Window = new RenderWindow(new VideoMode(1366, 768), "Space shooter", Styles.Default);
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
            Globals.startMenuTextures.Add(new Texture("Assets/Menus/StartMenu/start.png"));
            Globals.startMenuTextures.Add(new Texture("Assets/Menus/StartMenu/end.png"));

            Globals.shipTextures.Add(new Texture("Assets/Sprites/Ships/smallorange.png"));

            Globals.starTexture = new Texture("Assets/Sprites/background/flare.png");

            Globals.bulletTextures.Add(new Texture("Assets/Sprites/Bullets/laser.png"));

            Globals.asteroidTextures.Add(new Texture("Assets/Sprites/Asteroids/asteroid.png"));
        }
    }
}
