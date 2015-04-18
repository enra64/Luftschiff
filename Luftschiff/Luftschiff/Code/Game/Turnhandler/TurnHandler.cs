using System;
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

        private void _addCrewTarget(CrewMember _cre, Room target, Room currentRoom)
        {
            _crewTargets.Add(new CrewTarget(_cre,target,0,true));
        }

        public void addCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            if (targetRoom != crewMember.CurrentRoom)
            {
                _addCrewTarget(crewMember, targetRoom, crewMember.CurrentRoom); 
            }



            /*
            
//track job
crewMember.HasJob = true;
List<Room> a = _areaReference.getRooms();
// start calculate possible length of way
int roomsOnXWay = (int) ((crewMember.CurrentRoom.Position.X - targetRoom.Position.X)/crewMember.CurrentRoom.getRect().Width);
int roomsOnYWay = (int) ((crewMember.CurrentRoom.Position.Y - targetRoom.Position.Y)/crewMember.CurrentRoom.getRect().Height);
int emergeout = 0;
Room Iterator = crewMember.CurrentRoom;

for (int i = 0; i < Iterator._nearRooms.Count ; i++)
{
    if (Iterator._nearRooms.ElementAt(i) == targetRoom)
    {
        Console.WriteLine();
        _crewTargets.Add(new CrewTarget(crewMember, targetRoom, 1, true));
        emergeout = 3;
    }
}

while ((roomsOnXWay != 0 || roomsOnYWay != 0 )&& emergeout != 3)
{
    bool check = true;
    // check if room is nearby
    for (int i = 0; i < Iterator._nearRooms.Count && check; i++)
    {
        if (Iterator._nearRooms.ElementAt(i) == targetRoom)
        {
            _crewTargets.Add(new CrewTarget(crewMember, targetRoom, 1, true));
            check = false;
            emergeout = 3;
        }  
    }

    //try to figure out in whoch position is one possible direction to get to the target room 
    int k;
    for ( k = 0; k < Iterator._nearRooms.Count && check; k++)
    {
        if (roomsOnYWay < 0 && Iterator.Position.Y - Iterator._nearRooms.ElementAt(k).Position.Y > 0)
        {
            _crewTargets.Add(new CrewTarget(crewMember, Iterator._nearRooms.ElementAt(k), (int)Math.Abs(roomsOnXWay) + (int)Math.Abs(roomsOnYWay), false));
            check = false;
            roomsOnYWay++;
        }
        if (roomsOnYWay > 0 && -1 * (Iterator.Position.Y - Iterator._nearRooms.ElementAt(k).Position.Y) > 0)
        {
            _crewTargets.Add(new CrewTarget(crewMember, Iterator._nearRooms.ElementAt(k), (int)Math.Abs(roomsOnXWay) + (int)Math.Abs(roomsOnYWay), false));
            check = false;
            roomsOnYWay--;
        }
        if (roomsOnXWay < 0 && Iterator.Position.X - Iterator._nearRooms.ElementAt(k).Position.X > 0)
        {
            _crewTargets.Add(new CrewTarget(crewMember, Iterator._nearRooms.ElementAt(k), (int)Math.Abs(roomsOnXWay) + (int)Math.Abs(roomsOnYWay), false));
            check = false;
            roomsOnXWay++;
        }
        if (roomsOnXWay > 0 && -1 * (Iterator.Position.X - Iterator._nearRooms.ElementAt(k).Position.X) > 0)
        {
            _crewTargets.Add(new CrewTarget(crewMember, Iterator._nearRooms.ElementAt(k), (int)Math.Abs(roomsOnXWay) + (int)Math.Abs(roomsOnYWay), false));
            check = false;
            roomsOnXWay--;
        }
    }
    if (check)
    {
        emergeout ++;
    }
    else
    {
    Console.WriteLine("raum geaddet");
      Iterator = Iterator._nearRooms.ElementAt(k); 
    }


}
 */


            //TODO: jan-ole: improve pathfinding algorithm
            //do by adding a target to the crewTargetsList
            //_crewTargets.Add(new CrewTarget(crewMember, targetRoom, 2));
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
            foreach (var c in _weaponTargets) {
                if (c.NeededActions == 0)
                    c.FiringRoom.inflictDamage(c.Target, true);
                c.NeededActions--;
            }

            foreach (var c in _crewTargets)
            {
                if(c.NeededActions == 0)
                    c.Crew.moveToRoom(c.Target);
                if(c.NeededActions < 0)
                    throw new IndexOutOfRangeException("action not removed!");
                //invalid for finished actions to be able to clean it up
                if (c.NeededActions == 0)
                {
                    //if the crewmember has arrived at its target, 
                    if (c.IsLastAction)
                    {
                        //make the crew do the appropriate action at its target
                        c.Target.OnCrewArrive(c.Crew);
                        c.Crew.HasJob = false;
                    }
                    c.NeededActions = -42;
                }
                c.NeededActions--;
            }

            //remove targets with invalid neededactions count to collect garbage
            _crewTargets.RemoveAll(s => s.NeededActions < 0);
        }
    }
}
