
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

        public virtual void update(){
        }

        public void setTarget(List<Room> targets ,Vector2f clickposition){
            //TODO add calculation to determine, which room is clicke
            //targetRoom_= xyz;
        }
        public int getTarget()
        {
            return targetRoom_;
        }






    }
}
