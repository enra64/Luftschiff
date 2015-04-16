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

        //we begin ignoring all gamestate stuff until i want to use it
        //public static Globals.EGameStates currentState = Globals.EGameStates.fuckedUp;
        public static ProtoGameState main, sub;

        static void Main(string[] args)
        {
            Initializer.initialize();

            loadState(Globals.EStateSelection.main, Globals.EGameStates.game);

            while (Window.IsOpen)
            {
                if (sub != null)
                    sub.mainUpdate();
                else
                    main.mainUpdate();
                
                
                if (sub != null)
                    sub.draw();
                else
                    main.draw();
            }
        }

        public static void loadState(Globals.EStateSelection which, Globals.EGameStates targetState)
        {
            if (which == Globals.EStateSelection.main)
            {
                switch (targetState)
                {
                    case Globals.EGameStates.game:
                        main = new Game();
                        break;
                }
            }
            else if (which == Globals.EStateSelection.sub)
            {
                switch (targetState)
                {
                }
            }
        }

        /// <summary>
        /// this method name was not chosen because it sounds cool.
        /// </summary>
        /// <param name="twoButtonDialog"></param>
        public static bool injectDialog(Dialog dialog)
        {
            if (sub == null)
            {
                sub = dialog;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void killDialog()
        {
            sub = null;
        }
    }
}
