using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Weapons;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class AirCannonRoom : Room
    {
        private Projectile cannonball;
        public AirCannonRoom(Vector2f position) : base(position)
        {
            loadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.AirCannon);
        }
        public override void update()
        {
            if(cannonball != null)
            cannonball.update();
        }

        public override void draw()
        {
            base.draw();
            if(cannonball != null)
                cannonball.draw();
        }

        public override void inflictDamage(Monster monster, bool hits)
        {
            monster.getTurnDamage(0, true);
            cannonball = new Projectile(monster.Center, this.Center);
            Console.WriteLine("cannon inflicts damage");
        }
    }
}
