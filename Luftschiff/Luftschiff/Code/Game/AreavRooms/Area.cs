using System;
using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Game.Crew;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Area : Entity
    {
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
        private static List<CrewMember> crew_ ;

        public Area()
        {
            rooms_ = new List<Room>();
            crew_ = new List<CrewMember>();
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
        public void AddRoom(Room a)
        {
            //look for near rooms and save them in list
            FloatRect work = new FloatRect();
            for (int i = 0; i < rooms_.Count; i++)
            {
                work = rooms_.ElementAt(i).getRect();
                work.Height = work.Height + 50; // pixel differenz
                work.Width = work.Width + 50;
                if (work.Intersects(a.getRect()))
                {
                    a.addNearRooms(rooms_.ElementAt(i));
                }
            }
            rooms_.Add(a);
        }

        public void AddCrewToRoom(Room r, CrewMember c) {
            r.setCrewInRoom(c);
            crew_.Add(c);
        }

        public void RemoveCrewFromRoom(Room r, CrewMember c) {
            r.RemoveCrewMember(c);
            crew_.Remove(c);
        }

        public override void update()
        {
            #region Check Rooms and Crew for Clicks
            if (MouseHandler.UnhandledClick)
            {
                Vector2f lastClickPosition = MouseHandler.LastClickPosition;
                bool clickWasLeft = MouseHandler.Left;
                //first, just detect what was clicked
                Room clickedRoom = null;
                CrewMember clickedCrew = null;
                
                //check crew for click
                foreach (CrewMember c in crew_)
                    if (c.CheckClick(lastClickPosition))
                        clickedCrew = c;

                //check rooms for click
                if (clickedCrew == null)
                    foreach (Room r in rooms_)
                        if (r.CheckClick(lastClickPosition))
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
                        if(clickedRoom.IsAbleToTarget)
                            Cursor.CursorMode(Cursor.Mode.aim);
                        MouseHandler.UnhandledClick = false;
                    }
                        

                    //a crewmember was clicked -> mousehandler
                    if (clickedCrew != null)
                    {
                        MouseHandler.SelectedCrew = clickedCrew;
                        Cursor.CursorMode(Cursor.Mode.move);
                        MouseHandler.UnhandledClick = false;
                    }
                    
  
                    //nothing was clicked, remove the selection
                    if (clickedRoom == null && clickedCrew == null)
                    {
                        Cursor.CursorMode(Cursor.Mode.standard);
                        MouseHandler.SelectedCrew = null;
                        MouseHandler.SelectedRoom = null;
                    }
                }
                //right click
                else
                {
                    //a room was rightclicked, and a crew has been selected previously
                    if (clickedRoom != null && MouseHandler.SelectedCrew != null)
                    {
                        Console.WriteLine("crewmember will move or not");
                        MouseHandler.SelectedCrew.setTarget(clickedRoom);
                        MouseHandler.UnhandledClick = false;
                    }
                }
            }
            #endregion
            
            foreach(Room r in rooms_)
                r.update();
        }
        /// <summary>
        /// draws every room added to the area
        /// </summary>
        public override void draw()
        {
            for (int i = 0; i < rooms_.Count; i++)
            {
                rooms_.ElementAt(i).draw();
            }
            foreach (var room in rooms_)
                room.priorityDraw();
        }
    }
}
