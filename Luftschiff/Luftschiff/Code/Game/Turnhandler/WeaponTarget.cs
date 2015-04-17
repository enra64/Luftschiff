using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game.Turnhandler
{
    class WeaponTarget
    {
        /// <summary>
        /// reference to the room that will fire
        /// </summary>
        public Room FiringRoom;

        /// <summary>
        /// reference to the target that the room will fire upon
        /// </summary>
        public Monster Target;

        /// <summary>
        /// amount of actions the room will take until firing
        /// </summary>
        public int NeededActions;

        /// <summary>
        /// Target standard constructor.
        /// </summary>
        /// <param name="firingRoom">Firing Room reference</param>
        /// <param name="target">reference to the room that the crewmember will be moved to</param>
        /// <param name="neededActions"><para>the amount of base rounds the crew will take until this room,</para>
        /// so without buffs etc. as the turnhandler will take care of that</param>
        public WeaponTarget(Room firingRoom, Monster target, int neededActions)
        {
            FiringRoom = firingRoom;
            Target = target;
            NeededActions = neededActions;
        }
    }
}
