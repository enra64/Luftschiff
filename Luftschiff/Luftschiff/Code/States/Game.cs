﻿
using System;
using System.Collections.Generic;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;

using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;


namespace Luftschiff.Code.States {
    class Game : ProtoGameState
    {
        private Sprite _backgroundSprite;
        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game ()
        {
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);
        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {
            Controller.Window.Draw(_backgroundSprite);
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
            //Dialog examples
            /*
            //ok create a new listdialog
            List<String> testList= new List<String>();
            for(int i = 0; i < 10; i++)
                testList.Add("a"+i);
            
            ListDialog test = new ListDialog(testList, "message", "titletest");
            //return index of button in list
            Console.WriteLine(test.show());
            */
            /*
            //construct a yes / no dialog; Ja and Nein are also available as a smaller constructor
            TwoButtonDialog test2 = new TwoButtonDialog("jaKnopf", "neinKnopf", "Nachricht", "Titel");
            //show it, blocking all other execution until return of true/false
            Console.WriteLine(test2.show());
            */
        }
    }
}
