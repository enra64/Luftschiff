using System;
using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Projectiles;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Area : Object
    {
        public int Life { get; set; }
        private int _maxLife = 1000;
        private int _currentRoomButton = 0;
        private States.Game _gameReference = Globals.GameReference;
        private Sprite _staticMaintexture;

        public enum RoomTypes
        {
            AirCannon,
            AirEngine,
            AirHospital,
            AirLunch,
            Empty,
            GroundAirshipWorkshop,
            GroundBarracks,
            GroundMarketplace,
            GroundTavern
        }

        //list to fill with rooms;
        //rooms have the number of their position in list
        private List<Room> rooms_;
        public readonly List<CrewMember> CrewList = new List<CrewMember>();

        public Area()
        {
            //create lists
            rooms_ = new List<Room>();
            CrewList = new List<CrewMember>();
            Life = 1000;
            _staticMaintexture = new Sprite(new Texture(Globals.ShipTexture));
            _staticMaintexture.Position = new Vector2f(10, 550);
            _staticMaintexture.Scale = new Vector2f(1.1f, 0.9f);
            _staticMaintexture.Rotation = 270;
            /*
             * Hochkant Version
            _staticMaintexture = new Sprite(new Texture(Globals.ShipTexture));
            _staticMaintexture.Position = new Vector2f(10, 10);
            _staticMaintexture.Scale = new Vector2f(1.1f, 0.7f);
             */
        }

        /// <summary>
        /// return the left life of the ship, probably needs to be moved
        /// </summary>
        public float HealthPercent {
            get { return ((float)Life / (float)_maxLife) * 100; }
        }

        public int MovingCrew
        {
            get
            {
                //because this gets called async by the turnhandler very often, we must ensure valid collection values
                //and i's
                int moveCount = 0;
                for (int i = CrewList.Count - 1; i >= 0; i--)
                    //reduce i if invalid
                    if (CrewList[i < CrewList.Count ? i : --i].IsStillMoving)
                        moveCount++;
                return moveCount;
            }
        }

        /// <summary>
        /// get list of all rooms inserte in the area
        /// </summary>
        public List<Room> getRooms()
        {
            return rooms_;
        }

        /// <summary>
        /// add new room in an area
        /// </summary>
        public void AddRoom(Room newRoom)
        {
            //ERRORSOURCE 
            //look for near rooms and save them in list
            FloatRect work = new FloatRect();
            FloatRect work2 = new FloatRect();
            for (int i = 0; i < rooms_.Count; i++)
            {
                work = rooms_.ElementAt(i).GlobalRect;
                work.Height = work.Height + 125; // pixel differenz
                work.Top = work.Top - 75;
                work2 = rooms_.ElementAt(i).GlobalRect;
                work2.Width = work.Width + 125;
                work2.Left = work.Left - 75;

                if (work.Intersects(newRoom.GlobalRect) || work2.Intersects(newRoom.GlobalRect)) 
                {
                    newRoom.addNearRooms(rooms_.ElementAt(i));
                    rooms_.ElementAt(i).addNearRooms(newRoom);
                }
            }
            //increase room button for room keyboard shortcut system
            _currentRoomButton++;
            
            //give the room its key listener
            newRoom.AddKeyboardShortcut(_currentRoomButton);

            //add room to list
            rooms_.Add(newRoom);
        }

        /// <summary>
        ///     Call after adding all rooms
        /// </summary>
        public void FinalizeRooms()
        {
            //add the doors
            foreach (var r in rooms_)
                r.FinalizeTiles();
        }

        /// <summary>
        /// Adds a crewmember to a room and to the area, sets the crewmembers currentRoom correctly
        /// Activate DisableSetCrewInRoom to make the crew move slowly
        /// </summary>
        public void AddCrewToRoom(Room r, CrewMember c, bool DisableSetCrewInRoom)
        {
            //disable flag was set, so the crew will not instantly change position
            if (DisableSetCrewInRoom)
                r.SetCrewInRoom(c, true);
            //the flag to disable this call was not set.
            else
                r.SetCrewInRoom(c);
            CrewList.Add(c);
            c.CurrentRoom = r;
        }

        /// <summary>
        /// Removes a crewmember from the room and from the area, sets the crewmembers room to null
        /// </summary>
        public void RemoveCrewFromRoom(CrewMember c)
        {
            //only this crewlist.remove may exist to avoid bugs
            c.CurrentRoom.RemoveCrewMember(c);
            CrewList.Remove(c);
            Console.WriteLine("Crew removed from room "+c.CurrentRoom.GetType());
            //remove reference in mousehandler
            if(MouseHandler.SelectedCrew == c)
                MouseHandler.SelectedCrew = null;
            c.CurrentRoom = null;
        }

        /// <summary>
        /// Get a room to damage, used by the dragon to get one.
        /// </summary>
        /// <param name="position">-1 for random, valid values for specific</param>
        public Room GetRandomRoom(int position)
        {
            //return random room
            if (position < 0)
                return rooms_.ElementAt(new Random().Next(rooms_.Count));
            //return room at position else
            return rooms_.ElementAt(position%rooms_.Count);
        } 

        public void Update()
        {
            #region Check Rooms and Crew for Clicks
            if (MouseHandler.UnhandledClick){
                Vector2f lastClickPosition = MouseHandler.LastClickPosition;
                bool clickWasLeft = MouseHandler.Left;
                //first, just detect what was clicked
                Room clickedRoom = null;
                CrewMember clickedCrew = null;
                
                //check crew for click
                foreach (CrewMember c in CrewList)
                    if (c.IsClickInside(lastClickPosition))
                        clickedCrew = c;

                //check rooms for click
                if (clickedCrew == null)
                    foreach (Room r in rooms_)
                        if (r.IsClickInside(lastClickPosition))
                            clickedRoom = r;



                //any clicked rooms or crewmember have been detected by now

                //decide what to do with the clicked item
                //handle left clicks, they can only select
                if (clickWasLeft)
                {
                    //a room was clicked, set into mousehandler
                    if (clickedRoom != null)
                    {
                        MouseHandler.SelectedRoom = clickedRoom;
                        //Console.WriteLine("selected room");
                        MouseHandler.UnhandledClick = false;
                    }
                        

                    //a crewmember was clicked -> mousehandler
                    if (clickedCrew != null)
                    {
                        MouseHandler.SelectedCrew = clickedCrew;
                        MouseHandler.UnhandledClick = false;
                    }
                    
  
                    //nothing was clicked, remove the selection
                    if (clickedRoom == null && clickedCrew == null)
                    {
                        MouseHandler.SelectedCrew = null;
                        MouseHandler.SelectedRoom = null;
                        //ERRORSOURCE: consumes click events that hit nothing, area must be updated last
                        MouseHandler.UnhandledClick = false;
                    }
                }
                //right click
                else
                {
                    //a room was rightclicked, and a crew has been selected previously
                    if (clickedRoom != null && MouseHandler.SelectedCrew != null)
                    {
                        Console.WriteLine("crewmember will move or not");
                        //directly call turnhandler, moving control from the crew here
                        Globals.TurnHandler.AddCrewTarget(MouseHandler.SelectedCrew, clickedRoom);
                        MouseHandler.UnhandledClick = false;
                    }
                }
            }
            #endregion

            //remove dead rooms
            for (int i = 0; i < rooms_.Count; i++) {
                if (!rooms_[i].IsAlive) {
                    RemoveRoom(rooms_[i]);
                    i--;
                }
            }

            foreach (var r in rooms_)
                r.Update();

            foreach (var c in CrewList)
                c.Update();

            //show a "you died" dialog for now
            if (Life <= 0)
            {
                bool restart = new TwoButtonDialog("Nochmal starten?", "Du bist gestorben.").show();
                if(restart)
                    Controller.LoadState(Globals.EStates.game);
                else
                    Controller.Window.Close();
            }
            //ERRORSOURCE Remove crew on death
            for (int i = 0; i < CrewList.Count; i++)
            {
                if (CrewList.ElementAt(i)._health < 0)
                {
                    //crew dead, remove via area method. direct removements are bad, because they
                    //fail to do everything necessary.
                    RemoveCrewFromRoom(CrewList.ElementAt(i));
                    i--;
                }
            }

        }
        /// <summary>
        /// draws every room added to the area
        /// </summary>
        public override void Draw()
        {
            // draw ship over map in not beautiful 
            //TODO improve this a bit

            Controller.Window.Draw(_staticMaintexture);
            for (int i = 0; i < rooms_.Count; i++)
            {
                rooms_.ElementAt(i).Draw();
            }

            for (int k = 0; k < CrewList.Count; k++){
                CrewList.ElementAt(k).Draw();
            }
        }

        /// <summary>
        /// Called by the turnhandler, to switch a crewmembers room, since otherwise the crewmember
        /// would need to get a reference to the area
        /// </summary>
        /// <param name="moveAction">The Crewtarget the turnhandler wants executed, contains start and end room</param>
        public void RepositionCrew(CrewTarget moveAction) {
            RemoveCrewFromRoom(moveAction.Crew);
            AddCrewToRoom(moveAction.Target, moveAction.Crew, true);
            moveAction.Crew.Walk(moveAction);
        }

        public void RemoveRoom(Room a)
        {
            for (int k = 0; k < a._nearRooms.Count; k++)
            {
                a._nearRooms.ElementAt(k)._nearRooms.Remove(a);
            }
            rooms_.Remove(a);
            Globals.TurnHandler.InvalidateRoom(a);
            MouseHandler.SelectedRoom = null;
        }
    }
}
