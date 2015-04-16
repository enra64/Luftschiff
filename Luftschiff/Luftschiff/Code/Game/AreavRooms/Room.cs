using System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Room : Entity
    {
        /// <summary>
        /// kinds of possible Rooms
        /// </summary>
        public enum RoomKind
        {
        //adding roomkinds here

        EMPTY_ROOM,
        //kinds to use on the airships
        AIR_ENGINE_ROOM,
        AIR_HOSPITAL_WARD ,
        AIR_LUNCH_ROOM,
        AIR_CANNON_ROOM,


        //kinds to use on the ground
        GROUND_TAVERN,
        GROUND_AIRSHIP_WORKSHOP,
        GROUND_BARRACKS,
        GROUND_MARKETPLACE,

        }


        //List to use when Crew-class implemented 
        //List<Crew> crewList = new List<Crew>();

        //useful variables
        private int fireCounter_ = 0;
        private int cooldown_ = 0;
        private int life_ = 100;
        //save which kind the room is
        private RoomKind roomkind_;

        public Room()
        {
            roomkind_ = RoomKind.EMPTY_ROOM;
            //set sprites etc.
        }
        public Room(RoomKind kind)
        {
            switch (kind)
            {
                case (RoomKind.AIR_ENGINE_ROOM):
                    roomkind_ = RoomKind.AIR_ENGINE_ROOM;
                    //TODO insert code for engine here
                    break;
                case (RoomKind.AIR_HOSPITAL_WARD):
                    roomkind_ = RoomKind.AIR_HOSPITAL_WARD;
                    //TODO insert code for hospital here
                    break;
                case (RoomKind.AIR_LUNCH_ROOM):
                    roomkind_ = RoomKind.AIR_LUNCH_ROOM;
                    //TODO insert lunch room code here
                    break;
                case (RoomKind.AIR_CANNON_ROOM):
                    roomkind_ = RoomKind.AIR_CANNON_ROOM;
                    //TODO insert cannon code here
                    break;
                case (RoomKind.GROUND_TAVERN):
                    roomkind_ = RoomKind.GROUND_TAVERN;
                    //TODO insert tavern code here
                    break;
                case (RoomKind.GROUND_AIRSHIP_WORKSHOP):
                    roomkind_ = RoomKind.GROUND_AIRSHIP_WORKSHOP;
                    //TODO insert workshop code here
                    break;
                case (RoomKind.GROUND_BARRACKS):
                    roomkind_ = RoomKind.GROUND_BARRACKS;
                    //TODO insert barracks code here
                    break;
                case (RoomKind.GROUND_MARKETPLACE):
                    roomkind_ = RoomKind.GROUND_MARKETPLACE;
                    //TODO insert market placecode here
                    break;
                //TODO  init room sprites, different life states etc.
            }


        }



        public virtual void update()
        {

        }

        public void RoomAction(RoomKind RoomKind)
        {
            switch (RoomKind)
            {
                case (RoomKind.AIR_ENGINE_ROOM):
                    //TODO insert code for engine here
                    break;
                case (RoomKind.AIR_HOSPITAL_WARD):
                    //TODO insert code for hospital here
                    break;
                case (RoomKind.AIR_LUNCH_ROOM):
                    //TODO insert lunch room code here
                    break;
                case (RoomKind.AIR_CANNON_ROOM):
                    //TODO insert cannon code here
                    break;
                case (RoomKind.GROUND_TAVERN):
                    //TODO insert tavern code here
                    break;
                case (RoomKind.GROUND_AIRSHIP_WORKSHOP):
                    //TODO insert workshop code here
                    break;
                case (RoomKind.GROUND_BARRACKS):
                    //TODO insert barracks code here
                    break;
                case (RoomKind.GROUND_MARKETPLACE):
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
