using System;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Weapons {
    class CannonBall : Projectile
    {
        private readonly Vector2f _direction;
        private readonly Sprite s;
        private AnimatedSprite _explodingSprite;
        /// <summary>
        /// Set to true on Impact, used to handle sprite "deletion" and damage
        /// </summary>
        public bool HasMadeDamage { get; private set; }
        private bool _impactHappened = false, _playBool = true;

        private Monster _targetMonster;

        public CannonBall(Vector2f target, Vector2f startposition, Monster targetMonster)
        {
            Console.WriteLine("target position: " + target);
            _direction = (target - startposition) / 70;
            Position = startposition;
            s = new Sprite(Globals.CannonBallTexture);
            _targetMonster = targetMonster;
            HasMadeDamage = false;

            //Wall of Sprite :/
            _explodingSprite = new AnimatedSprite(Time.FromSeconds(0.1f),false,false,Position);
            Explosion = new Animation(Globals.Cannon_Explosion);
            Explosion.AddFrame(new IntRect(0,0,96,97));
            Explosion.AddFrame(new IntRect(97,0,96,97));
            Explosion.AddFrame(new IntRect(194,0,96,97));
            Explosion.AddFrame(new IntRect(291,0,96,97));
            Explosion.AddFrame(new IntRect(388,0,96,97));
            Explosion.AddFrame(new IntRect(0,96,96,97));
            Explosion.AddFrame(new IntRect(97,96,96,97));
            Explosion.AddFrame(new IntRect(194,96,96,97));
            Explosion.AddFrame(new IntRect(291,96,96,97));
            Explosion.AddFrame(new IntRect(388,96,96,97));
            Explosion.AddFrame(new IntRect(0,192,96,97));
            Explosion.AddFrame(new IntRect(97,192,96,97));
            Explosion.AddFrame(new IntRect(194,192,96,97));
            Explosion.AddFrame(new IntRect(291,192,96,97)); 
            Explosion.AddFrame(new IntRect(388,192,96,97));
        }

        public override void update()
        {
            if(!_impactHappened)
                Position += _direction;
            s.Position = Position;

            if (_impactHappened)
                SpritePlay();
                
        }

        public override FloatRect getRect()
        {
            return s.GetGlobalBounds();
        }

        public override void draw()
        {
            //only draw when not interacting and if no damage has been made yet
            if(!_impactHappened && !HasMadeDamage)
                Controller.Window.Draw(s);
            //draw the explosion sprite on impact
            if(_impactHappened)
                Controller.Window.Draw(_explodingSprite);
        }

        public  Animation Explosion { set; get; }

        /// <summary>
        /// execute on impact
        /// </summary>
        public override void OnImpact()
        {
            _impactHappened = true;
            //make damage
            if (!HasMadeDamage){
                //execute damage no monster, have to decide that better in the future (turnhandler maybe)
                _targetMonster.getTurnDamage(0, true);
                HasMadeDamage = true;
            }
        }

        /// <summary>
        /// Plays Sprite once on Impact
        /// </summary>
        public void SpritePlay()
        {
                _explodingSprite.Position = Position;
                _explodingSprite.Update(Globals.FRAME_TIME);
                if(_explodingSprite.TimesPlayed*2 <= Explosion.GetSize())
                    _explodingSprite.Play(Explosion);
                else
                    _impactHappened = false;
                if (_playBool){
                    new Sound(Globals.Boom).Play();
                    _playBool = false;
                }
            }
        }
    }

