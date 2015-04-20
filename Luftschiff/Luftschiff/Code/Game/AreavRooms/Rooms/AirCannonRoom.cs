﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Weapons;
using Luftschiff.Code.Global;
using SFML.Audio;
using SFML.Graphics;
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
            tilekind = loadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.AirCannon);
            _nearRooms = new List<Room>();

            //add additonal sprite: weapon
            Sprite gunSprite = new Sprite(Globals.GunTexture);
            
            //guess position by using the position of the tiles
            gunSprite.Position = _tilemap[1, 1].Position;
            
            //make it look okey
            gunSprite.Scale = new Vector2f(.6f, .6f);

            //add to sprite list so it gets drawn automatically
            _additionalRoomSprites.Add(gunSprite);
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

        /// <summary>
        /// Called to inflict damage upon the monster; gives the cannonball to the monster,
        /// because that is the easiest way to include damage on hit
        /// </summary>
        public override void inflictDamage(Monster monster, bool hits){
            //add sfx
            cannonball = new CannonBall(monster.Center, Center, monster);
            Collider.AddProjectile(cannonball);
            new Sound(Globals.CannonSound).Play();
        }
    }
}
