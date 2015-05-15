using Luftschiff.Code;
using Luftschiff.Code.Global;
using Luftschiff.Code.States;
using Luftschiff.Code.States.Fights;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff
{
    internal class Controller
    {
        private static Clock _frameClock;
        private static ProtoGameState main;
        public static RenderWindow Window { get; set; }
        public static View View { get; set; }

        private static void Main(string[] args)
        {
            _frameClock = new Clock();
            Initializer.Initialize();

            LoadState(Globals.EStates.menu);

            while (Window.IsOpen)
            {
                Window.SetFramerateLimit(100);
                Window.SetVerticalSyncEnabled(true);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    LoadState(Globals.EStates.menu);

                main.mainUpdate();
                main.draw();


                //FRAME_TIME always last!!
                Globals.FRAME_TIME = _frameClock.Restart();
            }
        }

        public static void LoadState(Globals.EStates targetState)
        {
            switch (targetState)
            {
                case Globals.EStates.graphicstest:
                    main = new GraphicsTest();
                    break;
                case Globals.EStates.menu:
                    main = new Menu();
                    break;
                case Globals.EStates.openworld:
                    main = new OpenWorld();
                    break;
                case Globals.EStates.batfight:
                    main = new BatFight();
                    break;
                case Globals.EStates.dragonfight:
                    main = new DragonFight();
                    break;
                case Globals.EStates.whalefight:
                    main = new WhaleFight();
                    break;
                case Globals.EStates.petuniefight:
                    main = new PetunieFight();
                    break;
            }
        }
    }
}