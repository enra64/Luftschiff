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
        private int _cannonLife = 70;
        private int _maxCannonLife = 70;

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

        public override void ReceiveRepair(int repairAmount, int repairSpecial)
        {
            base.ReceiveRepair(repairAmount, repairSpecial);
            //repair cannon with an appropriate amount
            _cannonLife = _cannonLife + repairSpecial > _maxCannonLife ? _maxCannonLife : _cannonLife + repairSpecial;
        }

        public override bool NeedsRepair { get { return RoomLife < MaxLife || _cannonLife < _maxCannonLife; } }

        public override void Update()
        {
            //needs to be called if we want a shortcut
            base.Update();

            //tint the cannon sprite to show damage
            foreach (var s in AdditionalRoomSprites)
            {
                byte notRedValue = (byte)((float)((float)_cannonLife / (float)_maxCannonLife) * 255f);
                s.Color = new Color(255, notRedValue, notRedValue);
            }
        }

        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            //only difference between this and a normal room is that we have a weapon that may get damaged and stop working
            _cannonLife -= 20;
        }

        /// <summary>
        ///     Called to inflict damage upon the monster; gives a projectile to the collider and plays a sound.
        ///     Does nothing when the cannon is dead
        /// </summary>
        public override void InflictDamage(Monster monster, bool hits)
        {
            //if the cannon has enough life left, it shoots
            if (_cannonLife <= 10) 
                return;
            
            //damage the dragon
            Globals.ColliderReference.AddProjectile(new CannonBall(this, monster));
            //sfx
            new Sound(Globals.CannonSound).Play();
        }


    }
}