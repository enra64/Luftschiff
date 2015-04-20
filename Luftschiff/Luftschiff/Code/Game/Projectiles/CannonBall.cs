using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Weapons
{
    internal class CannonBall : Projectile
    {
        private readonly Vector2f _direction;
        private readonly AnimatedSprite _explodingSprite;
        private readonly Monster _targetMonster;
        private bool _impactHappened, _playBool = true;

        public CannonBall(Vector2f target, Vector2f startposition, Monster targetMonster)
        {
            //init sprite first
            Sprite = new Sprite(Globals.CannonBallTexture);
            //set object position
            Position = startposition;

            //calculate direction
            _direction = (target - startposition)/70;

            //save monster for later
            _targetMonster = targetMonster;
            HasMadeDamage = false;

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

        /// <summary>
        ///     Set to true on Impact, used to handle sprite "deletion" and damage
        /// </summary>
        private bool HasMadeDamage { get; set; }

        private Animation Explosion { set; get; }

        public override void Update()
        {
            //move while impact has not happened
            if (!_impactHappened)
                Position += _direction;

            //stop moving, but play the sprite
            if (_impactHappened)
                SpritePlay();
        }

        public override void Draw()
        {
            //only draw when not interacting and if no damage has been made yet
            if (!_impactHappened && !HasMadeDamage)
                Controller.Window.Draw(Sprite);
            //draw the explosion sprite on impact
            if (_impactHappened)
                Controller.Window.Draw(_explodingSprite);
        }

        /// <summary>
        ///     execute on impact
        /// </summary>
        public override void OnImpact()
        {
            _impactHappened = true;
            //make damage
            if (!HasMadeDamage)
            {
                //execute damage to monster, have to decide that better in the future (turnhandler maybe)
                _targetMonster.getTurnDamage(0, true);
                HasMadeDamage = true;
            }
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
                _impactHappened = false;

            //play impact sound
            if (_playBool)
            {
                new Sound(Globals.Boom).Play();
                _playBool = false;
            }
        }
    }
}