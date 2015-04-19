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

        /// <summary>
        /// finds the hopefully shortest possible way to the chosen target room
        /// </summary>
        /// <param name="crewMember"></param>
        /// <param name="targetRoom"></param>
        public void addCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            //intialize variables
            Room work = crewMember.CurrentRoom;
            List<Room> way = new List<Room>();
            Room merk;
            way.Add(work);
            int whilebreaker = 0;
            float mindistance;
            //loop till target is found or whilebreaker says target is noct reachable
            while (!way.Contains(targetRoom)&& whilebreaker < 10)
            {
                // pseudo min distance to get it work 
                mindistance = 10000000;
                merk = work;
                // look after the attributes of all near rooms
                for (int i = 0; i < work._nearRooms.Count; i++)
                {
                    // break loop with target on last place
                    if (work._nearRooms.ElementAt(i) == targetRoom || merk == targetRoom)
                    {
                        merk = targetRoom;
                       // Console.WriteLine("found target");
                    }
                        // checks every not used room nearby if it is the closest to the target
                    else if (work._nearRooms.ElementAt(i).iswalkable()&&!way.Contains(work._nearRooms.ElementAt(i)))
                    {
                        Room possiblenext = work._nearRooms.ElementAt(i);
                        float distanceToTarget = (float)Global.Util.GetDistancebeweenVector2f(possiblenext.Position,targetRoom.Position);
                        //distance to target compare
                        if (distanceToTarget < mindistance)
                        {
                            mindistance = distanceToTarget;
                            merk = possiblenext;
                            //Console.WriteLine("Raum gemerkt");
                        }

                    }
                }
                //break for the loop if there is no possible way to go 
                if (merk == work)
                {
                    whilebreaker = 10;
                    //Console.WriteLine("needed whilbreaker");
                }
                    // add best possiblitiy to reach target 
                else
                {
                    way.Add(merk);
                    work = merk;
                   // Console.WriteLine("schleife zum weg hinzugefugt");
                } 
                whilebreaker++;
            }
            // sets the needed CrewTarget going through the list
            way.RemoveAt(0);
            for (int k = way.Count - 1; k >= 0 && whilebreaker != 10; k--)
            {
                if (k == way.Count - 1)
                {
                    Console.WriteLine(way.Count-k);
                    //Console.WriteLine("target");
                    _crewTargets.Add(new CrewTarget(crewMember, way.ElementAt(k),k, true));
                }
                else
                {
                    Console.WriteLine(way.Count - k);
                    //Console.WriteLine("waypoint");
                    _crewTargets.Add(new CrewTarget(crewMember, way.ElementAt(k),k, false));
                }
            }
        }
            //TODO: jan-ole: improve pathfinding algorithm for later problems


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
