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
                IntegerTilemap = LoadStandardTilekinds(2);
            }
        }

        public override void FinalizeTiles() {
            AddDoorsToTileArray();
            initializeTilemap(Area.RoomTypes.AirLunch);
        }
    }
}
