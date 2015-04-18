using System;
using SFML.Graphics;
using Luftschiff.Code;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;
using Luftschiff.Code.States;
using SFML.System;
using SFML.Window;

namespace Luftschiff
{
    class Controller
    {
        private static Clock _frameClock;
        public static RenderWindow Window { get; set; }
        public static View View { get; set; }

        static ProtoGameState main;

        static void Main(string[] args)
        {
            _frameClock = new Clock();
            Initializer.Initialize();

            LoadState(Globals.EStates.menu);

            while (Window.IsOpen)
            {
                Window.SetFramerateLimit(100);
                Window.SetVerticalSyncEnabled(true);

                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
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
                case Globals.EStates.game:
                    main = new Game();
                    break;
                case Globals.EStates.graphicstest:
                    main = new GraphicsTest();
                    break;
                case Globals.EStates.menu:
                    main = new Menu();
                    break;
            }
        }
    }
}
