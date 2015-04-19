using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.AreavRooms.Rooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Turnhandler;
using SFML.System;

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

        /// <summary>
        /// check whether there are any actions to be executed
        /// </summary>
        public bool HasStackedActions
        {
            get { return _crewTargets.Count > 0 || _weaponTargets.Count > 0; }
        }


        public void addCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            Room work = crewMember.CurrentRoom;
            List<Room> way = new List<Room>();
            Room merk;
            way.Add(work);
            int whilebreaker = 0;
            float mindistance;
            while (!way.Contains(targetRoom)&& whilebreaker < 10)
            {
                mindistance = 10000000;
                merk = work;
                for (int i = 0; i < work._nearRooms.Count; i++)
                {
                    if (work._nearRooms.ElementAt(i).iswalkable())
                    {
                        Room possiblenext = work._nearRooms.ElementAt(i);
                        float distanceToTarget = (float)Global.Util.GetDistancebeweenVector2f(possiblenext.Position,targetRoom.Position);
                        if (distanceToTarget < mindistance)
                        {
                            merk = possiblenext;
                            Console.WriteLine("Raum gemerkt");
                        }

                    }
                }
                if (merk == work)
                {
                    whilebreaker = 10;
                    Console.WriteLine("needed whilbreaker");
                }
                else
                {
                    way.Add(merk);
                    work = merk;
                    Console.WriteLine("schleife zum weg hinzugefugt");
                } 
                whilebreaker++;
            }
            for (int k = way.Count - 1; k >= 0 && whilebreaker != 10; k--)
            {
                if (k == way.Count - 1)
                {
                    _crewTargets.Add(new CrewTarget(crewMember, targetRoom, 1, true));
                }
                else
                {
                    _crewTargets.Add(new CrewTarget(crewMember,way.ElementAt(k),way.Count-1-k,false));
                }
            }
        }

        // nearest room int in list

            /*

        //TODO jan-ole : seltsamen shit irgendwie sinnvol zusammenbasteln please :**(
        private Room nearestRoom(Room current , Room target)
        {
            Room res = current;
            float mindistance=10000000;

            for (int i = 1; i < current._nearRooms.Count; i++)
            {
                if (i == 1)
                {
                    res = current._nearRooms.ElementAt(0);
                    mindistance = (float)Global.Util.GetDistancebeweenVector2f(current._nearRooms.ElementAt(0).Position, target.Position);
                }
                Room work = current._nearRooms.ElementAt(i);
                float distance =
                    (float) Global.Util.GetDistancebeweenVector2f(current._nearRooms.ElementAt(i).Position, target.Position);
                if (distance < mindistance)
                {
                    res = current._nearRooms.ElementAt(i);
                    mindistance = distance;
                }
            }
            return res;
        }

        private void _addCrewTarget(CrewMember _cre, Room target, Room currentRoom,int n)
        {
            if (n == 0)
            {
                _crewTargets.Add(new CrewTarget(_cre, currentRoom, 1, true));
                _addCrewTarget(_cre, target, nearestRoom(currentRoom, target), n + 1);
            }
            else
            {
                if (nearestRoom(currentRoom, target) == target)
                {
                    _crewTargets.Add(new CrewTarget(_cre, currentRoom, n, false));
                }
                else
                {
                    _crewTargets.Add(new CrewTarget(_cre,currentRoom,n,false));
                    _addCrewTarget(_cre,target,currentRoom,n+1);
                }
            }
  

        }

        public void addCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            Room Iter = crewMember.CurrentRoom;


if (targetRoom != crewMember.CurrentRoom)
{
    _addCrewTarget(crewMember, crewMember.CurrentRoom, targetRoom,0); 
}




            
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
            _weaponTargets.RemoveAll(s => s.NeededActions < 0);
        }
    }
}
