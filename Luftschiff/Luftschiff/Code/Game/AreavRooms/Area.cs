using System.Collections.Generic;
using System.Linq;
using Luftschiff.Code.Game.Crew;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Area : Entity
    {
        //list to fill with rooms;
        //rooms have the number of their position in list
        private List<Room> rooms_;
        private static List<CrewMember> crew_ ;

        public Area()
        {
            rooms_ = new List<Room>();
            crew_ = new List<CrewMember>();
        }
        public List<Room> getRooms()
        {
            return rooms_;
        }

        public void AddRoom(Room a)
        {
            rooms_.Add(a);
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

                //check rooms for clicks first, may be needed to be switched around
                //check rooms for click
                foreach (Room r in rooms_)
                    if (r.CheckClick(lastClickPosition))
                        clickedRoom = r;

                //check crew for click
                if(clickedRoom == null)
                    foreach (CrewMember c in crew_)
                        if (c.CheckClick(lastClickPosition))
                            clickedCrew = c;

                //any clicked rooms or crewmember have been detected by now

                //decide what to do with the clicked item
                //handle left clicks, they can only select
                if (clickWasLeft)
                {
                    //a room was clicked, set into mousehandler
                    if (clickedRoom != null)
                        MouseHandler.selectedRoom = clickedRoom;

                    //a crewmember was clicked -> mousehandler
                    if (clickedCrew != null)
                        MouseHandler.selectedCrew = clickedCrew;
  
                    //nothing was clicked, remove the selection
                    if (clickedRoom == null && clickedCrew == null)
                    {
                        MouseHandler.selectedCrew = null;
                        MouseHandler.selectedRoom = null;
                    }
                }
                //right click
                else
                {
                    //a room was rightclicked, and a crew has been selected previously
                    if (clickedRoom != null && MouseHandler.selectedCrew != null)
                    {
                        clickedCrew.setTarget(clickedRoom);
                    }
                }
            }
            #endregion
            
            for (int i = 0; i < crew_.Count; i++)
            {
                //TODO CrewTarget update and function to dertermine way
            }
        }

        public override void draw()
        {
            for (int i = 0; i < rooms_.Count; i++)
            {
                rooms_.ElementAt(i).draw();
            }
        }
    }
}
