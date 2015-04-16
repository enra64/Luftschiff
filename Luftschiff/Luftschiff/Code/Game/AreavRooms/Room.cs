using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Room : Entity
    {
        //kinds of possible Rooms
        public const int EMPTY_ROOM = 0;
        //kinds to use on the airships
        public const int AIR_ENGINE_ROOM = 1;
        public const int AIR_HOSPITAL_WARD = 2;
        public const int AIR_LUNCH_ROOM = 3;
        public const int AIR_CANNON_ROOM = 4;
        //adding roomkinds here

        //kinds to use on the ground
        public const int GROUND_TAVERN = 20;
        public const int GROUND_AIRSHIP_WORKSHOP = 21;
        public const int GROUND_BARRACKS = 22;
        public const int GROUND_MARKETPLACE = 23;

        //List to use when Crew-class implemented 
        //List<Crew> crewList = new List<Crew>();

        //useful variables
        private int fireCounter_ = 0;
        private int cooldown_ = 0;
        private int life_ = 100;
        //save which kind the room is
        private int roomkind_;

        public Room()
        {
            roomkind_ = EMPTY_ROOM;
            //set sprites etc.
        }
        public Room(int kind)
        {
            switch (kind)
            {
                case (AIR_ENGINE_ROOM):
                    roomkind_ = AIR_ENGINE_ROOM;
                    //TODO insert code for engine here
                    break;
                case (AIR_HOSPITAL_WARD):
                    roomkind_ = AIR_HOSPITAL_WARD;
                    //TODO insert code for hospital here
                    break;
                case (AIR_LUNCH_ROOM):
                    roomkind_ = AIR_LUNCH_ROOM;
                    //TODO insert lunch room code here
                    break;
                case (AIR_CANNON_ROOM):
                    roomkind_ = AIR_CANNON_ROOM;
                    //TODO insert cannon code here
                    break;
                case (GROUND_TAVERN):
                    roomkind_ = GROUND_TAVERN;
                    //TODO insert tavern code here
                    break;
                case (GROUND_AIRSHIP_WORKSHOP):
                    roomkind_ = GROUND_AIRSHIP_WORKSHOP;
                    //TODO insert workshop code here
                    break;
                case (GROUND_BARRACKS):
                    roomkind_ = GROUND_BARRACKS;
                    //TODO insert barracks code here
                    break;
                case (GROUND_MARKETPLACE):
                    roomkind_ = GROUND_MARKETPLACE;
                    //TODO insert market placecode here
                    break;
                //TODO  init room sprites, different life states etc.
            }


        }



        public virtual void update()
        {

        }

        public void RoomAction(int RoomKind)
        {
            switch (RoomKind)
            {
                case(AIR_ENGINE_ROOM):
                    //TODO insert code for engine here
                    break;
                case(AIR_HOSPITAL_WARD):
                    //TODO insert code for hospital here
                    break;
                case(AIR_LUNCH_ROOM):
                    //TODO insert lunch room code here
                    break;
                case(AIR_CANNON_ROOM):
                    //TODO insert cannon code here
                    break;
                case(GROUND_TAVERN):
                    //TODO insert tavern code here
                    break;
                case(GROUND_AIRSHIP_WORKSHOP):
                    //TODO insert workshop code here
                    break;
                case(GROUND_BARRACKS):
                    //TODO insert barracks code here
                    break;
                case(GROUND_MARKETPLACE):
                    //TODO insert market placecode here
                    break;
                //TODO add useful cooldown after action;
            }
        }
        public Boolean checkClick()
        {
            //TODO insert code to check clicks
            return false;
        }

        public void receiveDamage(int damage)
        {
            life_ = life_ - damage;
            if (fireCounter_ > 0)
            {
                life_ = life_ - 10;  //template int for fire damage 
            }
            //TODO special damage for Crewdamage
        }
        public void setOnFire(int RoundsRoomIsBurning)
        {
            this.fireCounter_ = RoundsRoomIsBurning;
        }



    }
}
