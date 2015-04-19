using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class GroundBarracks : Room
    {

        public GroundBarracks(Vector2f position) : base(position)
        {
            _nearRooms = new List<Room>();
        }

        public override void update()
        {
        }
    }
}
