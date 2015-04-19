using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Weapons;
using Luftschiff.Code.Global;
using SFML.Audio;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class AirCannonRoom : Room
    {
        private CannonBall cannonball;

        //override this to signify that this room gets the aim cursor
        public override bool IsAbleToTarget
        {
            get { return true; }
        }

        public AirCannonRoom(Vector2f position) : base(position)
        {
            {
                tilekind = loadStandardTilekinds(2);
                initializeTilemap(Area.RoomTypes.AirCannon);
                _nearRooms = new List<Room>();
            }
        }

        public override void update()
        {
            if(cannonball != null)
                cannonball.update();
        }

        //this gets called after the usual draw so that cannonballs etc can be drawn on top of other rooms
        public override void priorityDraw()
        {
            if(cannonball != null)
                cannonball.draw();
        }


        public override void inflictDamage(Monster monster, bool hits){
            monster.getTurnDamage(0, true);
            //add sfx
            cannonball = new CannonBall(monster.Center, Center);
            Collider.AddProjectile(cannonball);
            new Sound(Globals.CannonSound).Play();
            //Console.WriteLine("cannon inflicts damage");
        }
    }
}
