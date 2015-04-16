
using System;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;

using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;


namespace Luftschiff.Code.States {
    class Game : ProtoGameState {

        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game ()
        {

        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {

        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill() {
            
        }

        /// <summary>
        /// This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update(){
            //Dialog example
            /*
            //ok create a new dialog
            TwoButtonDialog test = new TwoButtonDialog("test Tag", 
             "yes button string", 
             "no button string",
             "messagetest", 
             "titletest");
            
            //show the dialog. this will block the execution until a return value
            //can be obtained
            test.show();
            
            //extract the result with getResultIsPositive
            String console = test.getResultIsPositive() ? "clicked yes" : "clicked no";
            Console.Write(console);
            */
        }
    }
}
