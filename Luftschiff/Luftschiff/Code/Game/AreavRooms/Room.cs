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

        //useful variables
        private int _fireCounter = 0;
        private int _cooldown = 0;
        private int _life = 100;
        private bool _walkAble = true;
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

    }
}
