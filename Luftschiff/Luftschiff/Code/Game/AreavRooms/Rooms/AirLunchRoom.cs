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
    class AirLunchRoom : Room
    {
        public AirLunchRoom(Vector2f position)
            : base(position)
        {
            {
                tilekind = loadStandardTilekinds(2);
                initializeTilemap(Area.RoomTypes.AirLunch);
            }
        }
        public override void update()
        {
        }
    }
}
