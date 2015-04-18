
using System;
using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.AreavRooms.Rooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Game.Weapons;
using Luftschiff.Code.Global;

using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;


namespace Luftschiff.Code.States {
    class Game : ProtoGameState
    {
        private Sprite _backgroundSprite;
        public Monster CurrentMonster;
        private Button turnButton;

        //test
        private Area test;
        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game ()
        {
            Controller.Window.SetMouseCursorVisible(false);
            //Test data 
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);
            CurrentMonster = new Dragon(Globals.DragonTexture);
            test = Globals.AreaReference;
            test.AddRoom(new AirCannonRoom(new Vector2f(75, 200)));
            test.AddRoom(new AirEngineRoom(new Vector2f(75, 350)));
            test.AddRoom(new AirHospitalWard(new Vector2f(75, 500)));
            test.AddRoom(new AirLunchRoom(new Vector2f(225, 275)));
            test.AddRoom(new EmptyRoom(new Vector2f(225, 450)));
            test.AddCrewToRoom(test.getRooms().ElementAt(0), new CrewMember(test.getRooms().ElementAt(0)));
            Collider.AddMonster(CurrentMonster);

            turnButton = new Button("Turn finished!", new Vector2f(Controller.Window.Size.X / 2, Controller.Window.Size.Y - 40), new Vector2f(100, 40));

        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {
            Controller.Window.Draw(_backgroundSprite);
            CurrentMonster.draw(); 
            //test draw 
            test.draw();
            turnButton.draw();
        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill() {
            Controller.Window.SetMouseCursorVisible(true);
        }

        /// <summary>
        /// This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update()
        {
            CurrentMonster.update();
            test.update();

            //execute the turn when the user clicks the turn button
            if (turnButton.update()){
                Globals.TurnHandler.executeTurn();
            }

            //make the button another color to notify the user
            if(Globals.TurnHandler.HasStackedActions)
                turnButton.ForceAttention(true);
            else
                turnButton.ForceAttention(false);
        }
    }
}
