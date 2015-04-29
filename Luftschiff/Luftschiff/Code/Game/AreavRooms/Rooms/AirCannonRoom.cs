using System.Collections.Generic;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Global;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class AirCannonRoom : Room
    {
        public AirCannonRoom(Vector2f position) : base(position)
        {
            IntegerTilemap = LoadStandardTilekinds(1);
            initializeTilemap(Area.RoomTypes.AirCannon);

            //add additonal sprite: weapon
            var gunSprite = new Sprite(Globals.GunTexture);

            //guess position by using the position of the tiles
            gunSprite.Position = ObjectTilemap[1, 1].Position;

            //make it look okay
            gunSprite.Scale = new Vector2f(.6f, .6f);

            //add to sprite list so it gets drawn automatically
            AdditionalRoomSprites.Add(gunSprite);
        }

        //override this to signify that this room gets the aim cursor
        public override bool IsAbleToTarget
        {
            get { return true; }
        }

        public override void Update()
        {
            //needs to be called if we want a shortcut
            base.Update();
        }


        /// <summary>
        ///     Called to inflict damage upon the monster; gives the cannonball to the monster,
        ///     because that is the easiest way to include damage on hit
        /// </summary>
        public override void InflictDamage(Monster monster, bool hits)
        {
            //add sfx
            Globals.ColliderReference.AddProjectile(new CannonBall(this, monster));
            new Sound(Globals.CannonSound).Play();
        }


    }
}