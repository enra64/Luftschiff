using System;
using Luftschiff.Code.Game.Crew;
using System.Collections.Generic;
using Luftschiff.Code.Game.Monsters;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    abstract class Room : Entity
    {
        //List to use when Crew-class implemented 
        List<CrewMember> crewList = new List<CrewMember>();

        //useful variables
        private int _fireCounter = 0;
        private int _cooldown = 0;
        private int _life = 100;
        private bool _walkAble = true;
        //save which kind the room is

        /// <summary>
        /// this is called when a crewmember arrives in this room, and has no further rooms to go to
        /// </summary>
        public abstract void OnCrewArrive(CrewMember traveler);

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
        /// Called by the turnhandler to get the damage dealt by that room
        /// </summary>
        public abstract void inflictDamage(Monster monster, bool hits);

    }
}
