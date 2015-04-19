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
        /// <summary>
        /// holds the cannonball of this room if available
        /// </summary>
        private CannonBall _cannonball;

        /// <summary>
        /// This returns true for rooms that should get the aim cursor,
        /// and should get checked by the monster to detect whether it needs to add this room
        /// to the turnhandler
        /// </summary>
        public override bool IsAbleToTarget
        {
            get { return true; }
        }

        /// <summary>
        /// Standard room constructor, give the top left position of where it should spawn
        /// </summary>
        /// <param name="position"></param>
        public AirCannonRoom(Vector2f position) : base(position)
        {
            {
                tilekind = loadStandardTilekinds(2);
                initializeTilemap(Area.RoomTypes.AirCannon);
                _nearRooms = new List<Room>();
            }
        }

        /// <summary>
        /// Entity update override
        /// </summary>
        public override void update()
        {
            //currently only move the cannonball if existent
            if(_cannonball != null)
                _cannonball.update();
        }

        /// <summary>
        /// this gets called after the usual draw so that cannonballs etc can be drawn on top of other rooms
        /// </summary>
        public override void priorityDraw()
        {
            if(_cannonball != null)
                _cannonball.draw();
        }

        /// <summary>
        /// This 
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="hits"></param>
        public override void inflictDamage(Monster monster, bool hits){
            monster.getTurnDamage(0, true);
            //add sfx
            _cannonball = new CannonBall(monster.Center, Center);
            Collider.AddProjectile(_cannonball);
            new Sound(Globals.CannonSound).Play();
            //Console.WriteLine("cannon inflicts damage");
        }
    }
}
