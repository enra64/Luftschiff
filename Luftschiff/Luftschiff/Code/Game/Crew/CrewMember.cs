
using Luftschiff.Code.Game.AreavRooms;
using SFML.System;
using System.Collections.Generic;
namespace Luftschiff.Code.Game.Crew {
    class CrewMember : Entity{
        //possible abilities
        //TODO add or remove abilities
        private int health_ = 100;
        private int actionPoints_ = 1;
        private int repairSpeed_ = 1;
        private int slackFireSpeed_ = 1;
        private int weaponSkills_ = 1;
        private int targetRoom_ =0;


        public CrewMember() { }

        public override void update(){
        }

        public void setTarget(List<Room> targets ,Vector2f clickposition){
            //TODO add calculation to determine, which room is clicke
            //targetRoom_= xyz;
        }
        public int getTarget()
        {
            return targetRoom_;
        }

        //just created this because it was easy, if this is not usable change the
        //call in area in the mouse click handler
        public void setTarget(Room clickedRoom)
        {
            throw new System.NotImplementedException();
        }

        //move to that room NAOW
        public void moveToRoom(Room targetRoom)
        {
            
        }
    }
}
