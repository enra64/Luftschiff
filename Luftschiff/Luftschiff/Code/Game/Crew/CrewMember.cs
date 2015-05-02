
using System;
using Luftschiff.Code.Game.AreavRooms;
using SFML.System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using SFML.Graphics;

namespace Luftschiff.Code.Game.Crew {
    class CrewMember : Entity
    {
        private RectangleShape _indicatorShape;
        private Sprite useAnAnimatedSprite;
        public Room CurrentRoom{ get; set; }

        //possible abilities
        //TODO add or remove abilities
        public int _health{get;set;}
        private int _actionPoints = 1;
        private int _repairSpeed = 4;
        private int _slackFireSpeed = 1;
        private int _weaponSkills = 1;
        private int _targetRoom = 0;


        public CrewMember(Room firstRoom)
        {
            CurrentRoom = firstRoom;
            useAnAnimatedSprite = new Sprite(Globals.CrewTexture);
            useAnAnimatedSprite.Scale = new Vector2f(.2f, .2f);
            _health = 100;

            //init indicator rectangle
            Vector2f size = new Vector2f(useAnAnimatedSprite.Scale.X*useAnAnimatedSprite.Texture.Size.X,
                useAnAnimatedSprite.Scale.Y*useAnAnimatedSprite.Texture.Size.Y);
            _indicatorShape = new RectangleShape(size)
            {
                Position = useAnAnimatedSprite.Position,
                FillColor = Color.Transparent,
                OutlineColor = Color.Transparent,
                OutlineThickness = 2
            };
        }

        public void StartSelectionIndicator()
        {
            _indicatorShape.OutlineColor = Color.Green;
            new System.Threading.Timer(obj => { _indicatorShape.OutlineColor = Color.Transparent; }, null, (long) 400, System.Threading.Timeout.Infinite);
        }

        public void Draw()
        {
            // damage sign for crew health debug
            Shape test = new CircleShape(10);
            test.Position = useAnAnimatedSprite.Position;
            test.FillColor = new Color(20, 0, 0, (byte)(255 - (_health / 100f) * 255));  
            if(_health <= 0)
                test.FillColor = Color.Red; 
            Controller.Window.Draw(test);

            Controller.Window.Draw(useAnAnimatedSprite);
            Controller.Window.Draw(_indicatorShape);
        }

        public override FloatRect getRect()
        {
            return useAnAnimatedSprite.GetGlobalBounds();
        }

        /// <summary>
        ///     Repairs the CurrentRoom by a certain amount
        /// </summary>
        public void RepairRoom()
        {
            //debug output
            Console.WriteLine("repairing");
            CurrentRoom.ReceiveRepair(10 * _repairSpeed, 8 * _repairSpeed);
        }

        /// <summary>
        ///     Reduces the currentRoom FireLife by a certain amount
        /// </summary>
        public void SlackFire()
        {
            Console.WriteLine("slacking fire");
            //start animation
            //detect impssible fire life values
            if (CurrentRoom.FireLife > 0)
                CurrentRoom.FireLife -= 1*_slackFireSpeed;
        }

        /// <summary>
        /// gets called when the crewmember should just work the room, as in use the cannon or whatever
        /// </summary>
        public void WorkRoom()
        {
            Console.WriteLine("working in room");
            //animation
        }

        public override void Update(){
        }

        public void setPosition(Vector2f newPosition)
        {
            useAnAnimatedSprite.Position = newPosition;
            _indicatorShape.Position = newPosition;
        }
    }
}
