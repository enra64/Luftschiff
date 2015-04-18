
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
        private Room currentRoom = null;
        public Room CurrentRoom{ get; set; }

        //possible abilities
        //TODO add or remove abilities
        private int _health = 100;
        private int _actionPoints = 1;
        private int _repairSpeed = 1;
        private int _slackFireSpeed = 1;
        private int _weaponSkills = 1;
        private int _targetRoom = 0;
        public bool HasJob = false;


        public CrewMember(Room firstRoom)
        {
            currentRoom = firstRoom;
            useAnAnimatedSprite = new Sprite(Globals.CrewTexture);
        }

        public override void draw()
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
        public int RepairRoom()
        {
            //should start repair animation
            return 10 * _repairSpeed;
        }

        /// <summary>
        /// returns the amount of fire kill by this crewmember
        /// </summary>
        public int SlackFire()
        {
            //start animation
            return 10*_slackFireSpeed;
        }

        /// <summary>
        /// gets called when the crewmember should just work the room, as in use the cannon or whatever
        /// </summary>
        public int WorkRoom()
        {
            //animation
            int whatever = 0;
            return whatever;
        }

        public override void update(){
        }

        public void setTarget(List<Room> targets ,Vector2f clickposition){
            //TODO add calculation to determine, which room is clicke
            //targetRoom_= xyz;
        }

        public int getTarget()
        {
            return _targetRoom;
        }

        //just created this because it was easy, if this is not usable change the
        //call in area in the mouse click handler
        public void setTarget(Room clickedRoom)
        {
            Globals.TurnHandler.addCrewTarget(this, clickedRoom);
        }

        public void setPosition(Vector2f newPosition)
        {
            useAnAnimatedSprite.Position = newPosition;
        }

        //move to that room NAOW
        public void moveToRoom(Room targetRoom)
        {
            Globals.AreaReference.RemoveCrewFromRoom(currentRoom, this);
            currentRoom = targetRoom;
            Globals.AreaReference.AddCrewToRoom(targetRoom, this);
        }
    }
}
