using System;
using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Turnhandler;
using Luftschiff.Code.Global;

namespace Luftschiff.Code.Game
{
    /// <summary>
    ///     The class that queues the user scheduled actions
    /// </summary>
    internal class TurnHandler
    {
        //references to other important classes via the global for convenience
        private readonly Area _areaReference = Globals.AreaReference;
        private readonly States.Game _gameReference = Globals.GameReference;
        //action lists
        private readonly List<CrewTarget> _crewTargets;
        private readonly List<WeaponTarget> _weaponTargets;

        public TurnHandler()
        {
            _crewTargets = new List<CrewTarget>();
            _weaponTargets = new List<WeaponTarget>();
        }

        /// <summary>
        ///     check whether there are any actions to be executed
        /// </summary>
        public bool HasStackedActions
        {
            get { return _crewTargets.Count > 0 || _weaponTargets.Count > 0; }
        }

        /// <summary>
        ///     finds the hopefully shortest possible way to the chosen target room
        /// </summary>
        /// <param name="crewMember">The crewmember to move</param>
        /// <param name="targetRoom">The room to send the crewmember to</param>
        //TODO: jan-ole: improve pathfinding algorithm for later problems
        public void AddCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            //ERRORSOURCE  weird crew movements 
            //intialize variables
            var work = crewMember.CurrentRoom;
            var way = new List<Room>();
            Room merk;
            way.Add(work);
            var whilebreaker = 0;
            float mindistance;
            //loop till target is found or whilebreaker says target is noct reachable
            while (!way.Contains(targetRoom) && whilebreaker < 10)
            {
                // pseudo min distance to get it work 
                mindistance = 10000000;
                merk = work;
                // look after the attributes of all near rooms
                for (var i = 0; i < work._nearRooms.Count; i++)
                {
                    // break loop with target on last place
                    if (work._nearRooms.ElementAt(i) == targetRoom || merk == targetRoom)
                    {
                        merk = targetRoom;
                        // Console.WriteLine("found target");
                    }
                    // checks every not used room nearby if it is the closest to the target
                    else if (work._nearRooms.ElementAt(i).iswalkable() && !way.Contains(work._nearRooms.ElementAt(i)))
                    {
                        var possiblenext = work._nearRooms.ElementAt(i);
                        var distanceToTarget =
                            (float) Util.GetDistancebeweenVector2f(possiblenext.Position, targetRoom.Position);
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
            for (var k = way.Count - 1; k >= 0 && whilebreaker != 10; k--)
            {
                if (k == way.Count - 1)
                {
                    Console.WriteLine(way.Count - k);
                    //Console.WriteLine("target");
                    _crewTargets.Add(new CrewTarget(crewMember, way.ElementAt(k), k, true));
                }
                else
                {
                    Console.WriteLine(way.Count - k);
                    //Console.WriteLine("waypoint");
                    _crewTargets.Add(new CrewTarget(crewMember, way.ElementAt(k), k, false));
                }
            }
        }

        /// <summary>
        ///     Adds a target for the room, so the turnhandler initializes an attack in ending this turn
        /// </summary>
        public void AddWeaponTarget(Room shootyPointy, Monster monster)
        {
            _weaponTargets.Add(new WeaponTarget(shootyPointy, monster, 0));
        }

        /// <summary>
        ///     execute all actions for this turn, and call the monster attack
        /// </summary>
        public void ExecuteTurn()
        {
            //kk now execute all the actions
            foreach (var c in _weaponTargets)
            {
                if (c.NeededActions == 0)
                    c.FiringRoom.inflictDamage(c.Target, true);
                c.NeededActions--;
            }

            foreach (var c in _crewTargets)
            {
                if (c.NeededActions == 0)
                {
                    _areaReference.RepositionCrew(c.Crew, c.Target);
                }
                if (c.NeededActions < 0)
                    throw new IndexOutOfRangeException("action not removed!");
                //invalid for finished actions to be able to clean it up
                if (c.NeededActions == 0)
                {
                    //if the crewmember has arrived at its target
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

            //start dragon attack
            ExecuteMonsterAttack();
        }

        private void ExecuteMonsterAttack()
        {
            _gameReference.CurrentMonster.makeTurnDamage();
        }
    }
}