﻿
using System;
using Luftschiff.Code.Game.AreavRooms;
using SFML.System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using SFML.Graphics;

namespace Luftschiff.Code.Game.Crew {
    class CrewMember : Entity
    {
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
            _health = 100;
        }

        public void Draw()
        {
            Controller.Window.Draw(useAnAnimatedSprite);
        }

        public override FloatRect getRect()
        {
            return useAnAnimatedSprite.GetGlobalBounds();
        }

        /// <summary>
        /// returns the amount of room heal by this crewmember
        /// </summary>
        public void RepairRoom()
        {
            Console.WriteLine("repairing");
            //should start repair animation
            int repairAmount = 10*_repairSpeed;
            if (CurrentRoom.RoomLife + repairAmount > CurrentRoom.MaxLife)
                CurrentRoom.RoomLife = CurrentRoom.MaxLife;
            else
                CurrentRoom.RoomLife += 10 * _repairSpeed;
        }

        /// <summary>
        /// returns the amount of fire kill by this crewmember
        /// </summary>
        public void SlackFire()
        {
            Console.WriteLine("slacking fire");
            //start animation
            CurrentRoom.FireLife -= 10*_slackFireSpeed;
        }

        /// <summary>
        /// gets called when the crewmember should just work the room, as in use the cannon or whatever
        /// </summary>
        public int WorkRoom()
        {
            Console.WriteLine("working in room");
            //animation
            int whatever = 0;
            return whatever;
        }

        public override void Update(){
        }

        public void setTarget(List<Room> targets ,Vector2f clickposition){
            //TODO add calculation to determine, which room is clicke
            //targetRoom_= xyz;
        }

        public int getTarget()
        {
            return _targetRoom;
        }

        public void setPosition(Vector2f newPosition)
        {
            useAnAnimatedSprite.Position = newPosition;
        }
    }
}
