using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;

namespace Luftschiff.Code.Game
{
    class CrewTarget
    {
        /// <summary>
        /// reference to the crew that will be moved
        /// </summary>
        public CrewMember Crew;

        /// <summary>
        /// reference to the room that the crewmember will be moved to
        /// </summary>
        public Room Target;

        /// <summary>
        /// <para>the amount of base rounds the crew will take until this room,</para>
        /// so without buffs etc. as the turnhandler will take care of that
        /// </summary>
        public int NeededActions;

        /// <summary>
        /// the standard constructor for a target.
        /// </summary>
        /// <param name="c">reference to the crew that will be moved</param>
        /// <param name="target">reference to the room that the crewmember will be moved to</param>
        /// <param name="neededActions"><para>the amount of base rounds the crew will take until this room,</para>
        /// so without buffs etc. as the turnhandler will take care of that</param>
        public CrewTarget(CrewMember c, Room target, int neededActions)
        {
            Crew = c;
            Target = target;
            NeededActions = neededActions;
        }
    }
}
