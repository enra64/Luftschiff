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
            Cursor.Initialize();
        }

        private static void InitializeAssets()
        {
            //compat settings for jan-ole
            Globals.BackgroundTexture = Texture.MaximumSize > 1024 ? new Texture("Assets/Graphics/testbg_big.png") : new Texture("Assets/Graphics/testbg.png");
           
            Globals.CrewTexture = new Texture("Assets/Graphics/crew.png");
            Globals.Cannon_Explosion = new Texture("Assets/Graphics/explosion-sprite.png");
            Globals.DragonTexture = new Texture("Assets/Graphics/dragon2.png");

            Globals.EngineTexture = new Texture("Assets/Graphics/engine.png");
            Globals.GunTexture = new Texture("Assets/Graphics/CannonRoom/cannon2_hq.png");
            Globals.HospitalTexture = new Texture("Assets/Graphics/hospitalwardspecial.png");

            Globals.FireBallTexture = new Texture("Assets/Graphics/fireball.png");
            Globals.CannonBallTexture = new Texture("Assets/Graphics/cannonball.png");

            Globals.TileFloor = new Texture("Assets/Graphics/groundtile.png");
            Globals.TileSpecial = new Texture("Assets/Graphics/special_tile.png");
            Globals.TileWall = new Texture("Assets/Graphics/walltile.png");

            //Cursor
            Globals.CursorAim = new Texture("Assets/Graphics/Cursors/aimCursor.png");
            Globals.CursorStandard = new Texture("Assets/Graphics/Cursors/standardCursor.png");
            Globals.CursorCrewMove = new Texture("Assets/Graphics/Cursors/moveCursor.png");

            //audio
            Globals.CannonSound = new SoundBuffer("Assets/Audio/GunShot.wav");
            Globals.ClickSound = new SoundBuffer("Assets/Audio/buttonclick.flac");
            Globals.Boom = new SoundBuffer("Assets/Audio/cannon_boom.wav");
        }
    }
}
