using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;

namespace Luftschiff.Code.Game
{
    internal class CrewTarget
    {
        /// <summary>
        ///     reference to the crew that will be moved
        /// </summary>
        public readonly CrewMember Crew;

        /// <summary>
        ///     Contains whether this is the last room the crewmember has to go to
        /// </summary>
        public readonly bool IsLastAction;

        /// <summary>
        ///     Save the room we come from to make calculating the door easier
        /// </summary>
        public readonly Room Origin;

        /// <summary>
        ///     reference to the room that the crewmember will be moved to
        /// </summary>
        public readonly Room Target;

        /// <summary>
        ///     <para>the amount of base rounds the crew will take until this room,</para>
        ///     so without buffs etc. as the turnhandler will take care of that
        /// </summary>
        public int WaitingTurns;

        /// <summary>
        ///     the standard constructor for a target.
        /// </summary>
        /// <param name="c">reference to the crew that will be moved</param>
        /// <param name="target">reference to the room that the crewmember will be moved to</param>
        /// <param name="waitingTurns">
        ///     <para>the amount of base rounds the crew will take until this room,</para>
        ///     so without buffs etc. as the turnhandler will take care of that
        /// </param>
        /// <param name="isLastAction">whether the crewmember is at its target in this room</param>
        public CrewTarget(CrewMember c, Room origin, Room target, int waitingTurns, bool isLastAction)
        {
            Crew = c;
            Target = target;
            Origin = origin;
            WaitingTurns = waitingTurns;
            IsLastAction = isLastAction;
        }
    }
}