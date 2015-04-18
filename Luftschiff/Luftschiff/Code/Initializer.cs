using SFML.Graphics;
using SFML.Window;

using Luftschiff.Graphics.Lib;
using Luftschiff.Code;
using Luftschiff.Code.Game;
using Luftschiff.Code.Game.AreavRooms;
using SFML.Audio;

namespace Luftschiff {
    static class Initializer {
        public static void Initialize() {
            InitializeWindow();
            InitializeMouse();
            InitializeAssets();
            InitializeMisc();
        }

        private static void InitializeMouse() {
            Controller.Window.MouseButtonPressed += MouseHandler.Click;
            Controller.Window.MouseWheelMoved += MouseHandler.Scroll;
            Controller.Window.MouseButtonReleased += MouseHandler.Release;
            Controller.Window.MouseMoved += MouseHandler.Move;
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

            //all the more or less static classses
            Globals.AreaReference = new Area();
            Globals.TurnHandler = new TurnHandler(Globals.AreaReference);

            Cursor.Initialize();
        }

        private static void InitializeAssets()
        {
            //compat settings for jan-ole
            Globals.BackgroundTexture = Texture.MaximumSize > 1024 ? new Texture("Assets/Graphics/testbg_big.png") : new Texture("Assets/Graphics/testbg.png");
            Globals.DragonTexture = new Texture("Assets/Graphics/dragon.png");
            Globals.CrewTexture = new Texture("Assets/Graphics/crew.png");

            Globals.CannonBallTexture = new Texture("Assets/Graphics/cannonball.png");

            Globals.TileTextures = new Texture[4];
            Globals.TileTextures[0] = new Texture("Assets/Graphics/groundtile.png");
            Globals.TileTextures[1] = new Texture("Assets/Graphics/groundtile.png");
            Globals.TileTextures[2] = new Texture("Assets/Graphics/special_tile.png");
            Globals.TileTextures[3] = new Texture("Assets/Graphics/walltile.png");

            //audio
            Globals.CannonSound = new SoundBuffer("Assets/Audio/GunShot.wav");
        }
    }
}
