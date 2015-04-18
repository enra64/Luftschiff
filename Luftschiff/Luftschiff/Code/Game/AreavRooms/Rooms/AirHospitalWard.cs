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
    class AirHospitalWard : Room
    {
        public AirHospitalWard(Vector2f position) : base(position)
        {
            loadStandardTilekinds(2);
            initializeTilemap(Area.RoomTypes.AirHospital);
        }
        public override void update()
        {
            
        }
        public override void inflictDamage(Monster monster, bool hits)
        {
            
        }
    }
}
