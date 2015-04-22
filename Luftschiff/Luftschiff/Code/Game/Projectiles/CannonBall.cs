using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Weapons
{
    internal class CannonBall : KineticProjectile
    {
        private readonly AnimatedSprite _explodingSprite;
        private bool _playBool = true;

        public CannonBall(Room startRoom, Monster targetMonster) : base(targetMonster, startRoom, Globals.CannonBallTexture)
        {
            //Wall of Sprite :/
            _explodingSprite = new AnimatedSprite(Time.FromSeconds(0.1f), false, false, Position);
            Explosion = new Animation(Globals.Cannon_Explosion);
            Explosion.AddFrame(new IntRect(0, 0, 96, 97));
            Explosion.AddFrame(new IntRect(97, 0, 96, 97));
            Explosion.AddFrame(new IntRect(194, 0, 96, 97));
            Explosion.AddFrame(new IntRect(291, 0, 96, 97));
            Explosion.AddFrame(new IntRect(388, 0, 96, 97));
            Explosion.AddFrame(new IntRect(0, 96, 96, 97));
            Explosion.AddFrame(new IntRect(97, 96, 96, 97));
            Explosion.AddFrame(new IntRect(194, 96, 96, 97));
            Explosion.AddFrame(new IntRect(291, 96, 96, 97));
            Explosion.AddFrame(new IntRect(388, 96, 96, 97));
            Explosion.AddFrame(new IntRect(0, 192, 96, 97));
            Explosion.AddFrame(new IntRect(97, 192, 96, 97));
            Explosion.AddFrame(new IntRect(194, 192, 96, 97));
            Explosion.AddFrame(new IntRect(291, 192, 96, 97));
            Explosion.AddFrame(new IntRect(388, 192, 96, 97));
        }

        private Animation Explosion { set; get; }

        public override void Update()
        {
            //move while impact has not happened
            if (!ImpactHappened)
                Position += Direction;
            //stop moving, but play the sprite
            else
                SpritePlay();
        }

        public override bool ShouldKill { get; set; }

        public override void Draw()
        {
            //only draw when not interacting and if no damage has been made yet
            if (!ImpactHappened && !HasMadeDamage)
                Controller.Window.Draw(Sprite);
            //draw the explosion sprite on impact
            if (ImpactHappened)
                Controller.Window.Draw(_explodingSprite);
        }

        /// <summary>
        ///     Gets executed while the projectile is over its targetMonster
        /// </summary>
        public override void WhileImpacting()
        {
            
        }

        /// <summary>
        ///     Plays Sprite once on Impact
        /// </summary>
        private void SpritePlay()
        {
            //update position to cannonball impact position
            _explodingSprite.Position = Position;

            //do magic
            _explodingSprite.Update(Globals.FRAME_TIME);
            if (_explodingSprite.TimesPlayed*2 <= Explosion.GetSize())
                _explodingSprite.Play(Explosion);
            else
            {
                //i have no idea why this is needed, but it is.
                ImpactHappened = false;
                ShouldKill = true;
            }

            //play impact sound
            if (_playBool)
            {
                new Sound(Globals.Boom).Play();
                _playBool = false;
            }
        }
    }
}