using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code;
using Luftschiff.Code.Global;
using Luftschiff.Code.Gamestates;

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

            loadState();//Globals.EStateSelection.main, Globals.EGameStates.startscreen);

            while (Window.IsOpen)
            {
                main.mainUpdate();
                if (sub != null)
                    sub.mainUpdate();

                main.draw();
                if (sub != null)
                    sub.draw();
            }
        }

        public static void loadState()//Globals.EStateSelection which, Globals.EGameStates targetState)
        {
            main = new Game();
            //ok ignore this too
            /*
            if (which == Globals.EStateSelection.main)
            {
                switch (targetState)
                {
                    case Globals.EGameStates.game:
                        main = new Game();
                        break;
                    case Globals.EGameStates.endscreen:
                        main = new EndScreen();
                        break;
                    case Globals.EGameStates.startscreen:
                        main = new Startscreen();
                        break;
                }
            }
            else if (which == Globals.EStateSelection.sub)
            {
                switch (targetState)
                {
                }
            }
             * */
        }
    }
}
