using System.Collections.Generic;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game
{
    /// <summary>
    /// The class that queues the user scheduled actions
    /// </summary>
    class TurnHandler
    {
        private Area _areaReference;
        private List<CrewTarget> crewTargets; 

        public TurnHandler(Area areaReference)
        {
            _areaReference = areaReference;
        }

        public void addCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            //TODO: jan-ole: add crew pathfinding algorithm    
            //do by adding a target to the crewTargetsList
            //crewTargets.Add(new CrewTarget(crewMember, targetRoom, 2));
        }

        public void addCrewPath(CrewMember crewMember, List<Room> path)
        {
            
        }

        public void addRoomTarget(Room shootyPointy, Monster monter)
        {
            
        }

        public void executeTurn()
        {
            //kk now execute all the actions
        }
    }
}
