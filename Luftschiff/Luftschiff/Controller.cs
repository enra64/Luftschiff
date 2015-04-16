using SFML.Graphics;
using Luftschiff.Code;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;
using Luftschiff.Code.States;

namespace Luftschiff
{
    class Controller
    {
        public static RenderWindow Window { get; set; }
        public static View View { get; set; }

        static ProtoGameState main;

        static void Main(string[] args)
        {
            Initializer.initialize();

            loadState(Globals.EGameStates.game);

            while (Window.IsOpen)
            {
                main.mainUpdate();
                main.draw();
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
