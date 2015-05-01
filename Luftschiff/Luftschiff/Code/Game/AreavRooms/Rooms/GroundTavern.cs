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
    class GroundTavern : Room
    {

        public GroundTavern(Vector2f position) : base(position)
        {
        }
        public override void FinalizeTiles() {
            //AddDoorsToTileArray();
            //initializeTilemap(Area.RoomTypes.AirHospital);
        }
    }
}
