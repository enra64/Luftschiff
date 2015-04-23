using System.Collections.Generic;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Game.Weapons;
using Luftschiff.Code.Global;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class AirCannonRoom : Room
    {
        private CannonBall _cannonball;

        public AirCannonRoom(Vector2f position) : base(position)
        {
            tilekind = loadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.AirCannon);

            //add additonal sprite: weapon
            var gunSprite = new Sprite(Globals.GunTexture);

            //guess position by using the position of the tiles
            gunSprite.Position = _tilemap[1, 1].Position;

            //make it look okay
            gunSprite.Scale = new Vector2f(.6f, .6f);

            //add to sprite list so it gets drawn automatically
            _additionalRoomSprites.Add(gunSprite);
        }

        //override this to signify that this room gets the aim cursor
        public override bool IsAbleToTarget
        {
            get { return true; }
        }

        public override void Update()
        {
            if (_cannonball != null)
                _cannonball.Update();
        }

        //this gets called after the usual draw so that cannonballs etc can be drawn on top of other rooms
        public override void priorityDraw()
        {
            if (_cannonball != null)
                _cannonball.Draw();
        }

        /// <summary>
        ///     Called to inflict damage upon the monster; gives the cannonball to the monster,
        ///     because that is the easiest way to include damage on hit
        /// </summary>
        public override void inflictDamage(Monster monster, bool hits)
        {
            //add sfx
            _cannonball = new CannonBall(this, monster);
            Collider.AddProjectile(_cannonball);
            new Sound(Globals.CannonSound).Play();
        }
    }
}