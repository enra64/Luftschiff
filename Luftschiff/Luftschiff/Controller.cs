using System;
using SFML.Graphics;
using Luftschiff.Code;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;
using Luftschiff.Code.States;
using SFML.System;

namespace Luftschiff
{
    class Controller
    {
        private static Clock frameClock;
        public static RenderWindow Window { get; set; }
        public static View View { get; set; }

        static ProtoGameState main;

        static void Main(string[] args)
        {
            frameClock = new Clock();
            Initializer.Initialize();

            loadState(Globals.EGameStates.game);

            while (Window.IsOpen)
            {
                main.mainUpdate();
                main.draw();
                Globals.FRAME_TIME = frameClock.Restart();
                Console.WriteLine(Globals.FRAME_TIME.AsSeconds());
            }
        }

        public static void loadState(Globals.EGameStates targetState)
        {
            switch (targetState)
            {
                case Globals.EGameStates.game:
                    main = new Game();
                    break;
            }
        }
    }
}
