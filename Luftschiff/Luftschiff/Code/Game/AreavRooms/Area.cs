using System.Collections.Generic;
using Luftschiff.Code.Game.Crew;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Area : Entity
    {
        //list to fill with rooms;
        //rooms have the number of their position in list
        private List<Room> rooms_ = new List<Room>();
        private List<CrewMember> crew_ = new List<CrewMember>();

        public List<Room> getRooms()
        {
            return rooms_;
        }

        public virtual void update()
        {
            for (int i = 0; i < crew_.Count; i++)
            {
                //TODO CrewTarget update and function to dertermine way
            }

        }


    }
}
