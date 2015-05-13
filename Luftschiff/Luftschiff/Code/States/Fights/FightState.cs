using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.AreavRooms.Rooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Global;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code.States.Fights
{
    internal abstract class FightState : ProtoGameState
    {
        private readonly Sprite _backgroundSprite;
        private readonly Area _currentArea;
        private readonly HealthBar _monsterBar;
        private readonly HealthBar _shipBar;
        private readonly Button _turnButton;

        public FightState()
        {
            Globals.GameReference = this;
            Globals.AreaReference = new Area();
            Globals.TurnHandler = new TurnHandler();
            Globals.ColliderReference = new Collider();

            //initialize List 
            CurrentMonsterList = new List<Monster>();

            //copy a reference to this class for convenience 
            _currentArea = Globals.AreaReference;


            //make the standard cursor invisible, since we do that ourselves 
            Controller.Window.SetMouseCursorVisible(false);


            //Test data  
            _backgroundSprite = new Sprite(Globals.BackgroundTexture);

            _currentArea.AddRoom(new AirCannonRoom(new Vector2f(570, 325)));
            _currentArea.AddRoom(new AirEngineRoom(new Vector2f(310, 245)));
            _currentArea.AddRoom(new AirHospitalWard(new Vector2f(440, 245)));
            _currentArea.AddRoom(new AirLunchRoom(new Vector2f(440, 390)));
            _currentArea.AddRoom(new EmptyRoom(new Vector2f(310, 390)));

            //add doors to the rooms 
            _currentArea.FinalizeRooms();


            //Test data for crewmembers 
            _currentArea.AddCrewToRoom(_currentArea.Rooms.ElementAt(0), new CrewMember(_currentArea.Rooms.ElementAt(0)),
                false);
            _currentArea.AddCrewToRoom(_currentArea.Rooms.ElementAt(0), new CrewMember(_currentArea.Rooms.ElementAt(0)),
                false);
            _currentArea.AddCrewToRoom(_currentArea.Rooms.ElementAt(0), new CrewMember(_currentArea.Rooms.ElementAt(0)),
                false);
            _currentArea.AddCrewToRoom(_currentArea.Rooms.ElementAt(0), new CrewMember(_currentArea.Rooms.ElementAt(0)),
                false);


            _currentArea.AddCrewToRoom(_currentArea.Rooms.ElementAt(1), new CrewMember(_currentArea.Rooms.ElementAt(1)),
                false);


            //init turnbutton with space for activation 
            _turnButton = new Button("Turn finished!",
                new Vector2f(Controller.Window.Size.X/2 - 60, Controller.Window.Size.Y - 40), new Vector2f(120, 40));
            _turnButton.ActivationKey = Keyboard.Key.Space;

            var healthBarSize = new Vector2f(Controller.Window.Size.X/2 - 40, 25);
            _monsterBar = new HealthBar(new Vector2f(Controller.Window.Size.X/2 + 20, 20), healthBarSize,
                Globals.HEALTH_BAR_COLOR_MONSTER);
            _shipBar = new HealthBar(new Vector2f(20, 20), healthBarSize, Globals.HEALTH_BAR_COLOR_SHIP);
        }

        public List<Monster> CurrentMonsterList { get; set; }

        /// <summary>
        ///     Main draw call for our Game.
        /// </summary>
        public override void draw()
        {
            Controller.Window.Draw(_backgroundSprite);

            //_currentArea area draw 
            _currentArea.Draw();

            //monster draw
            foreach (var CurrentMonster in CurrentMonsterList)
            {
                CurrentMonster.Draw();
            }

            //draw the turn button
            _turnButton.Draw();

            //health bars
            _monsterBar.Draw();
            _shipBar.Draw();

            //draw the bullets
            Globals.ColliderReference.Draw();

            //draw any notification
            Notifications.Instance.Draw();
        }

        /// <summary>
        ///     This method gets called when the Gamestate is about to be stopped
        ///     to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill()
        {
            Controller.Window.SetMouseCursorVisible(true);
            Globals.AreaReference = null;
            Globals.TurnHandler = null;
            Globals.GameReference = null;
        }

        /// <summary>
        ///     This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update()
        {
            foreach (var CurrentMonster in CurrentMonsterList)
            {
                CurrentMonster.Update();
            }

            //disable the turn button when prjectiles are flying
            _turnButton.Enable = Globals.ColliderReference.ProjectileCount == 0;

            //execute the turn when the user clicks the turn button
            if (_turnButton.Update())
            {
                Globals.TurnHandler.ExecuteTurn();
            }

            _shipBar.Update(Globals.AreaReference.HealthPercent);
            foreach (var CurrentMonster in CurrentMonsterList)
            {
                _monsterBar.Update(CurrentMonster.HealthPercent);
            }
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

            //update notifications
            Notifications.Instance.Update();

            //has to be updated last as it consumes all click events that hit nothing
            _currentArea.Update();
        }
    }
}