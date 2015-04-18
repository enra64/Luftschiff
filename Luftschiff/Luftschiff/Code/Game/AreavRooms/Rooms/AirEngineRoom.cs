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
    class AirEngineRoom : Room
    {
        public AirEngineRoom(Vector2f position)
            : base(position)
        {
            loadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.AirCannon);
        }
        public override void update()
        {
            throw new NotImplementedException();
        }
        public override void inflictDamage(Monster monster, bool hits)
        {
            throw new NotImplementedException();
        }
    }
}
