
using System;
using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.AreavRooms.Rooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Global;

using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;


namespace Luftschiff.Code.States {
    class Game : ProtoGameState
    {
        private Sprite _backgroundSprite;
        public Monster CurrentMonster;

        //test
        private Area test;
        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game ()
        {
            //Test data 
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);
            CurrentMonster = new Dragon(Globals.DragonTexture);
            test = Globals.AreaReference;
            test.AddRoom(new AirCannonRoom(new Vector2f(0, 0)));
            test.AddRoom(new AirEngineRoom(new Vector2f(150, 0)));
            test.AddRoom(new AirHospitalWard(new Vector2f(300, 0)));
            test.AddRoom(new AirLunchRoom(new Vector2f(0, 150)));
            test.AddRoom(new EmptyRoom(new Vector2f(0, 300)));
            test.AddCrewToRoom(test.getRooms().ElementAt(0), new CrewMember(test.getRooms().ElementAt(0)));
        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {
            Controller.Window.Draw(_backgroundSprite);
            //CurrentMonster.draw(); 
            // test draw 
            test.draw();
            
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
        public override void update()
        {
            CurrentMonster.update();
            test.update();
            
        }
    }
}
