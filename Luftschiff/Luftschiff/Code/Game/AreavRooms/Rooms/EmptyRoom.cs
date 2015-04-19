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
    class EmptyRoom : Room
    {
        public EmptyRoom(Vector2f position)
            : base(position)
        {
            tilekind = loadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.Empty);
            initializeTilemap(Area.RoomTypes.AirCannon);
            _nearRooms = new List<Room>();
        }
        public override void update()
        {
        }
        public override void inflictDamage(Monster monster, bool hits)
        {
            Console.WriteLine("empty inflicts damage");
        }
    }
}
