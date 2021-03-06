﻿using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game.Turnhandler
{
    internal class WeaponTarget
    {
        /// <summary>
        ///     reference to the room that will fire
        /// </summary>
        public Room FiringRoom;

        /// <summary>
        ///     reference to the target that the room will fire upon
        /// </summary>
        public Monster Target;

        /// <summary>
        ///     amount of actions the room will take until firing
        /// </summary>
        public int WaitingTurns;

        /// <summary>
        ///     Target standard constructor.
        /// </summary>
        /// <param name="firingRoom">Firing Room reference</param>
        /// <param name="target">reference to the room that the crewmember will be moved to</param>
        /// <param name="waitingTurns">
        ///     <para>the amount of base rounds the crew will take until this room,</para>
        ///     so without buffs etc. as the turnhandler will take care of that
        /// </param>
        public WeaponTarget(Room firingRoom, Monster target, int waitingTurns)
        {
            FiringRoom = firingRoom;
            Target = target;
            WaitingTurns = waitingTurns;
        }
    }
}