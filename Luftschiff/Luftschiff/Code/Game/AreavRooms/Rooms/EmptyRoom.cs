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
            IntegerTilemap = LoadStandardTilekinds(3);
        }
        public override void FinalizeTiles() {
            AddDoorsToTileArray();
            initializeTilemap(Area.RoomTypes.Empty);
        }
    }
}
