
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
        private HealthBar _monsterBar, _shipBar;

        //test
        private Area test;
        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game ()
        {
            Globals.GameReference = this;
            Controller.Window.SetMouseCursorVisible(false);
            //Test data 
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);
            CurrentMonster = new Dragon(Globals.DragonTexture);
            test = Globals.AreaReference;
            /*
            test.AddRoom(new AirCannonRoom(new Vector2f(75, 200)));
            test.AddRoom(new AirEngineRoom(new Vector2f(75, 350)));
            test.AddRoom(new AirHospitalWard(new Vector2f(75, 500)));
            test.AddRoom(new AirLunchRoom(new Vector2f(225, 275)));
            test.AddRoom(new EmptyRoom(new Vector2f(225, 450)));
            */
            test.AddRoom(new AirCannonRoom(new Vector2f(75, 200)));
            test.AddRoom(new AirCannonRoom(new Vector2f(75, 350)));
            test.AddRoom(new AirCannonRoom(new Vector2f(75, 500)));
            test.AddRoom(new AirCannonRoom(new Vector2f(225, 275)));
            test.AddRoom(new AirCannonRoom(new Vector2f(225, 450)));
            test.AddCrewToRoom(test.getRooms().ElementAt(0), new CrewMember(test.getRooms().ElementAt(0)));
            Collider.AddMonster(CurrentMonster);

            turnButton = new Button("Turn finished!", new Vector2f(Controller.Window.Size.X / 2, Controller.Window.Size.Y - 40), new Vector2f(100, 40));
            _monsterBar = new HealthBar(new Vector2f(Controller.Window.Size.X / 2 + 20, 20), new Vector2f(Controller.Window.Size.X / 2 - 40, 40));
            _shipBar = new HealthBar(new Vector2f(20, 20), new Vector2f(Controller.Window.Size.X / 2 - 40, 40));
        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {
            Controller.Window.Draw(_backgroundSprite);
            CurrentMonster.draw(); 
            //test area draw 
            test.draw();
            
            //draw the turn button
            turnButton.draw();

            //health bars
            _monsterBar.Draw();
            _shipBar.Draw();
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
            
            //execute the turn when the user clicks the turn button
            if (turnButton.update()){
                Globals.TurnHandler.executeTurn();
            }

            _shipBar.Update(Globals.AreaReference.HealthPercent);
            _monsterBar.Update(CurrentMonster.HealthPercent);

            //make the button another color to notify the user
            if (Globals.TurnHandler.HasStackedActions)
            {
                turnButton.ForceAttention = true;
                turnButton.ClickSound = true;
            }
            else
            {
                turnButton.ClickSound = false;
                turnButton.ForceAttention = false;
            }
            //has to be updated last as it consumes all click events that hit nothing
            test.update();
        }

        /// <summary>
        /// gets called by the turnhandler via globals, and should execute the monsters damage on the areas
        /// rooms
        /// </summary>
        public void ExecuteMonsterAttack()
        {
            //todo: write monster attacks
        }
    }
}
