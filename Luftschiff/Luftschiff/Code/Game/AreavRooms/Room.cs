using System;
using Luftschiff.Code.Game.Crew;
using System.Collections.Generic;
using Luftschiff.Code.Game.Monsters;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    abstract class Room : Entity
    {
        /// <summary>
        /// kinds of possible Rooms
        /// </summary>



        //List to use when Crew-class implemented 
        List<CrewMember> crewList = new List<CrewMember>();
        // List to save and get accses to rooms nearby
        protected List<Room> _nearRooms = new List<Room>();

        //useful variables
        protected int _fireCounter = 0;
        protected int _cooldown = 0;
        protected int _life = 100;
        protected bool _walkAble = true;
        protected int[,] tilekind = new int[4, 4];

        //save which kind the room is

        public abstract void RoomAction();

        public void ReceiveDamage(int damage)
        {
            _life = _life - damage;
            if (_fireCounter > 0)
            {
                _life = _life - 10;  //template int for fire damage 
            }
            //TODO special damage for Crewdamage
        }
        public void SetOnFire(int roundsRoomIsBurning)
        {
            this._fireCounter = roundsRoomIsBurning;
        }

        /// <summary>
        /// the monster has detected that this room has been selected
        /// and used to fire upon it
        /// </summary>
        public void inflictDamage(Monster monster, bool hits)
        {
            monster.getTurnDamage(0, hits);
        }

        /// <summary>
        /// the array is filled with a standart of numbers for the tilemap
        /// 0 -> empty map( everything 0)
        /// 1 -> border = 1 mid = 3 (roomspecific Item)
        /// </summary>
        public void loadStandartTilekinds(int[,] array, int kind)
        {
            switch (kind)
            {
                case(0):
                    array = new int[4, 4] {{0,0,0,0},
                                          {0,0,0,0},
                                          {0,0,0,0},
                                          {0,0,0,0}};
                    break;
                case(1):
                    array = new int[4,4] {{1,1,1,1},
                                          {1,3,3,1},
                                          {1,3,3,1},
                                          {1,1,1,1}};
                    break;
                case(2):  // i don't care that this case has no use atm
                    array = new int[4, 4] {{1,1,1,1},
                                          {1,3,3,1},
                                          {1,3,3,1},
                                          {1,1,1,1}};
                    break;

            }


        }

        public void addDoorsToTileArray(int[,] array, Vector2f position)
        {
            //TODO add door number to tileMap numbers
        }


    }
}
