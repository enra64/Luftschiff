using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Turnhandler;

namespace Luftschiff.Code.Game
{
    /// <summary>
    /// The class that queues the user scheduled actions
    /// </summary>
    class TurnHandler
    {
        private Area _areaReference;
        private List<CrewTarget> _crewTargets;
        private List<WeaponTarget> _weaponTargets; 

        public TurnHandler(Area areaReference)
        {
            _areaReference = areaReference;
            _crewTargets = new List<CrewTarget>();
            _weaponTargets = new List<WeaponTarget>();
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

        /// <summary>
        /// Calls the inflictDamage in shootyPointy on monster on this rounds end
        /// </summary>
        public void addRoomTarget(Room shootyPointy, Monster monster)
        {
            _weaponTargets.Add(new WeaponTarget(shootyPointy, monster, 0));
        }

        /// <summary>
        /// execute all actions for this turn
        /// </summary>
        public void executeTurn(){
            //kk now execute all the actions
            foreach (WeaponTarget c in _weaponTargets) {
                if (c.NeededActions == 0)
                    c.FiringRoom.inflictDamage(c.Target, true);
                c.NeededActions--;
            }

            foreach (CrewTarget c in _crewTargets)
            {
                if(c.NeededActions == 0)
                    c.Crew.moveToRoom(c.Target);
                c.NeededActions--;
            }
        }
    }
}
