using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //action lists
        private readonly List<CrewTarget> _crewActions = new List<CrewTarget>();
        private readonly States.Game _gameReference = Globals.GameReference;
        private readonly List<WeaponTarget> _weaponActions = new List<WeaponTarget>();

        //currently no constructor, because we do not do anything there anyway

        /// <summary>
        ///     check whether there are any actions to be executed
        /// </summary>
        public bool HasStackedActions
        {
            get { return _crewActions.Count > 0 || _weaponActions.Count > 0; }
        }

        /// <summary>
        ///     finds the hopefully shortest possible way to the chosen target room
        ///     Code-Magic courtesy of Jan-Ole
        /// </summary>
        /// <param name="crewMember">The crewmember to move</param>
        /// <param name="targetRoom">The room to send the crewmember to</param>
        //TODO: jan-ole: improve pathfinding algorithm for later problems
        public void AddCrewTarget(CrewMember crewMember, Room targetRoom)
        {
            //check the crew action list for existing entries for this crew, and clear if found
            _crewActions.RemoveAll(s => s.Crew == crewMember);

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
                    _crewActions.Add(new CrewTarget(crewMember, way.ElementAt(k), k, true));
                }
                else
                {
                    Console.WriteLine(way.Count - k);
                    //Console.WriteLine("waypoint");
                    _crewActions.Add(new CrewTarget(crewMember, way.ElementAt(k), k, false));
                }
            }
        }

        /// <summary>
        ///     Add a target for the room with 0 wait turns to execute immediately on turn end.
        ///     TODO: Arne: Accepting proposals for the room parameter name.
        /// </summary>
        public void AddWeaponTarget(Room shootyPointy, Monster monster)
        {
            _weaponActions.Add(new WeaponTarget(shootyPointy, monster, 0));
        }

        /// <summary>
        ///     execute all actions for this turn, and call the monster attack
        /// </summary>
        public void ExecuteTurn()
        {
            //deny turn when projectiles are still flying
            if (Collider.ProjectileCount > 0)
                return;

            //check all weapon targets, and shoot those with 0 waiting turns
            foreach (var c in _weaponActions){
                if (c.WaitingTurns == 0)
                    c.FiringRoom.inflictDamage(c.Target, true);
                //reduce waiting turns of all so that everything will be fired eventually,
                //and negative waiting turns can be nulled
                c.WaitingTurns--;
            }

            //check all crew targets
            foreach (var c in _crewActions)
            {
                //0 waiting turns on action -> execute action
                if (c.WaitingTurns == 0)
                    _areaReference.RepositionCrew(c.Crew, c.Target);
                //invalid for finished actions to be able to clean it up
                if (c.WaitingTurns == 0)
                {
                    //if the crewmember has arrived at its target
                    if (c.IsLastAction)
                    {
                        //make the crew do the appropriate action at its target
                        c.Target.OnCrewArrive(c.Crew);
                    }
                }
                //reduce wait turns for executed actions and actions to be executed
                c.WaitingTurns--;
            }

            //for each crew list check whether it has actions left
            foreach (CrewMember c in Globals.AreaReference.CrewList)
            {
                //count the amount of actions the turnhandler has saved for a crewmember
                int crewActionCount = _crewActions.Count(ca => ca.Crew == c);
                //the crew has no actions (moves) left, so oncrewarrive can
                //not have been executed yet, since the actions have not yet been
                //deleted
                if (crewActionCount == 0)
                {
                    //tell the room the crewmember is without a job, and that it should give him a job
                    c.CurrentRoom.OnCrewArrive(c);
                }
            }


            //remove targets with invalid neededactions count to collect garbage
            _crewActions.RemoveAll(s => s.WaitingTurns < 0);
            _weaponActions.RemoveAll(s => s.WaitingTurns < 0);


            Task.Run(() => ExecuteMonsterAttack());
        }

        /// <summary>
        /// Asnyc wait for the user attack to hit, then call the dragon attack
        /// </summary>
        private void ExecuteMonsterAttack()
        {
            //wait until the user projectiles arrived
            while (Collider.ProjectileCount > 0);
            //start dragon attack
            _gameReference.CurrentMonster.AttackShip(_areaReference);
        }
    }
}