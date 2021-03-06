﻿using Luftschiff.Code;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace Luftschiff
{
    internal static class Initializer
    {
        public static void Initialize()
        {
            InitializeWindow();
            InitializeMouse();
            InitializeAssets();
            InitializeMisc();
        }

        private static void InitializeMouse()
        {
            Controller.Window.MouseButtonPressed += MouseHandler.Click;
            Controller.Window.MouseWheelMoved += MouseHandler.Scroll;
            Controller.Window.MouseButtonReleased += MouseHandler.Release;
            Controller.Window.MouseMoved += MouseHandler.Move;
        }

        private static void InitializeWindow()
        {
            //initialize window
            Controller.Window = new RenderWindow(new VideoMode(1366, 768), "Luftschiff", Styles.Default);
            Controller.Window.SetVerticalSyncEnabled(true);
            Controller.Window.SetFramerateLimit(35);
            Controller.Window.Closed += delegate { Controller.Window.Close(); };

            //init view
            Controller.View = new View(new FloatRect(0, 0, Controller.Window.Size.X, Controller.Window.Size.Y));
            Controller.Window.SetView(Controller.View);
        }

        private static void InitializeMisc()
        {
            Globals.DialogFont = new Font("Assets/StandardFontSteamwreck.otf");
            Globals.NotificationFont = new Font("Assets/ANTSYPAN.TTF");
            Cursor.Initialize();
        }

        private static void InitializeAssets()
        {
            //compat settings for jan-ole
            Globals.BackgroundTexture = Texture.MaximumSize > 1024
                ? new Texture("Assets/Graphics/testbg_big.png")
                : new Texture("Assets/Graphics/testbg.png");

            Globals.CrewTexture = new Texture("Assets/Graphics/Elena/dude.png");
            Globals.DragonTexture = new Texture("Assets/Graphics/dragon2.png");
            Globals.SkywhaleTexture = new Texture("Assets/Graphics/wal.png");
            Globals.ShipTexture = new Texture("Assets/Graphics/Elena/Schiff.png");
            Globals.WhaleHornTexture = new Texture("Assets/Graphics/horn.png");
            Globals.BatTexture = new Texture("Assets/Graphics/bat.png");
            Globals.PetunienTexture = new Texture("Assets/Graphics/petunientop.png");

            //projectile impact fx
            Globals.Cannon_Explosion = new Texture("Assets/Graphics/explosion-sprite.png");
            Globals.RoomFireTexture = new Texture("Assets/Graphics/roomfire.png");

            //special room stuff
            Globals.EngineTexture = new Texture("Assets/Graphics/Elena/RoomSpecials/engine.png");
            Globals.GunTexture = new Texture("Assets/Graphics/Elena/RoomSpecials/kanonelined.png");
            Globals.HospitalTexture = new Texture("Assets/Graphics/Elena/RoomSpecials/medbay.png");

            //projectiles
            Globals.FireBallTexture = new Texture("Assets/Graphics/fireball.png");
            Globals.CannonBallTexture = new Texture("Assets/Graphics/cannonball.png");
            Globals.ClawTexture = new Texture("Assets/Graphics/claw.png");

            //tiles
            Globals.TileFloor = new Texture("Assets/Graphics/groundtile.png");
            Globals.TileSpecial = new Texture("Assets/Graphics/special_tile.png");
            Globals.TileWall = new Texture("Assets/Graphics/walltile.png");
            Globals.TileMetall = new Texture("Assets/Graphics/Elena/Tiles/metalltile.png");
            Globals.TileDoor = new Texture("Assets/Graphics/sign_browndoor.png");
            Globals.TileElWall = new Texture("Assets/Graphics/Elena/Tiles/mauer_black.png");

            //Cursor
            Globals.CursorAim = new Texture("Assets/Graphics/Cursors/aimCursor.png");
            Globals.CursorStandard = new Texture("Assets/Graphics/Cursors/standardCursor.png");
            Globals.CursorCrewMove = new Texture("Assets/Graphics/Cursors/moveCursor.png");

            //audio
            Globals.CannonSound = new SoundBuffer("Assets/Audio/GunShot.wav");
            Globals.ClickSound = new SoundBuffer("Assets/Audio/buttonclick.flac");
            Globals.BoomSound = new SoundBuffer("Assets/Audio/cannon_boom.wav");
            Globals.FireSound = new SoundBuffer("Assets/Audio/fire_sound.wav");
            Globals.FireCrackleSound = new SoundBuffer("Assets/Audio/firecrackle.wav");
        }
    }
}