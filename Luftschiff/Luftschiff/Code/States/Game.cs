
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.AreavRooms.Rooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Global;

using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Luftschiff.Code.States {
    class Game : ProtoGameState
    {
        private Sprite _backgroundSprite;
        public Monster CurrentMonster { get; set; }
        private Button _turnButton;
        private HealthBar _monsterBar, _shipBar;
        private Area _currentArea;

        //_currentArea
        /// <summary>
        /// The gamestate constructor.
        /// </summary>
        public Game (){
            //set references, initialize game lifecycle objects (these do get nulled in kill(), so we even manage our memory...)
            Globals.GameReference = this;
            Globals.AreaReference = new Area();
            Globals.TurnHandler = new TurnHandler();
            Globals.ColliderReference = new Collider();
            
            //copy a reference to this class for convenience
            _currentArea = Globals.AreaReference;

            //make the standard cursor invisible, since we do that ourselves
            Controller.Window.SetMouseCursorVisible(false);

            //Test data 
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);
            CurrentMonster = new Dragon();
            
            _currentArea.AddRoom(new AirCannonRoom(new Vector2f(75, 200)));
            _currentArea.AddRoom(new AirEngineRoom(new Vector2f(75, 350)));
            _currentArea.AddRoom(new AirHospitalWard(new Vector2f(75, 500)));
            _currentArea.AddRoom(new AirLunchRoom(new Vector2f(225, 275)));
            _currentArea.AddRoom(new EmptyRoom(new Vector2f(225, 450)));
            
            _currentArea.AddCrewToRoom(_currentArea.getRooms().ElementAt(0), new CrewMember(_currentArea.getRooms().ElementAt(0)));

            //init turnbutton with space for activation
            _turnButton = new Button("Turn finished!", new Vector2f(Controller.Window.Size.X / 2, Controller.Window.Size.Y - 40), new Vector2f(100, 40));
            _turnButton.ActivationKey = Keyboard.Key.Space;

            var healthBarSize = new Vector2f(Controller.Window.Size.X/2 - 40, 25);
            _monsterBar = new HealthBar(new Vector2f(Controller.Window.Size.X / 2 + 20, 20), healthBarSize, Globals.HEALTH_BAR_COLOR_MONSTER);
            _shipBar = new HealthBar(new Vector2f(20, 20), healthBarSize, Globals.HEALTH_BAR_COLOR_SHIP);
        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {
            Controller.Window.Draw(_backgroundSprite);
            
            //_currentArea area draw 
            _currentArea.Draw();
            
            //monster draw
            CurrentMonster.Draw(); 

            //draw the turn button
            _turnButton.Draw();

            //health bars
            _monsterBar.Draw();
            _shipBar.Draw();

            //draw the bullets
            Globals.ColliderReference.Draw();
        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill() {
            Controller.Window.SetMouseCursorVisible(true);
            Globals.AreaReference = null;
            Globals.TurnHandler = null;
            Globals.GameReference = null;
        }

        /// <summary>
        /// This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update()
        {
            CurrentMonster.Update();
            
            //execute the turn when the user clicks the turn button
            if (_turnButton.Update()){
                Globals.TurnHandler.ExecuteTurn();
            }

            _shipBar.Update(Globals.AreaReference.HealthPercent);
            _monsterBar.Update(CurrentMonster.HealthPercent);

            //make the button another color to notify the user
            if (Globals.TurnHandler.HasStackedActions)
            {
                _turnButton.ForceAttention = true;
                _turnButton.ClickSound = true;
            }
            else
            {
                _turnButton.ClickSound = false;
                _turnButton.ForceAttention = false;
            }

            //update the collider
            Globals.ColliderReference.Update();

            //has to be updated last as it consumes all click events that hit nothing
            _currentArea.Update();
        }
    }
}
