using System;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Weapons {
    class CannonBall : Projectile
    {
        private readonly Vector2f _direction;
        private readonly Sprite s;
        private AnimatedSprite _explodingSprite;
        private bool _interact = false;

        public CannonBall(Vector2f target, Vector2f startposition)
        {
            Console.WriteLine("target position: " + target);
            _direction = (target - startposition) / 70;
            Position = startposition;
            s = new Sprite(Globals.CannonBallTexture);

            //Wall of Sprite :/
            _explodingSprite = new AnimatedSprite(Time.FromSeconds(0.06f),false,false,Position);
            Explosion = new Animation(Globals.Cannon_Explosion);
            Explosion.AddFrame(new IntRect(0,0,97,96));
            Explosion.AddFrame(new IntRect(97,0,97,96));
            Explosion.AddFrame(new IntRect(194,0,97,96));
            Explosion.AddFrame(new IntRect(291,0,97,96));
            Explosion.AddFrame(new IntRect(388,0,97,96));
            Explosion.AddFrame(new IntRect(0,96,97,96));
            Explosion.AddFrame(new IntRect(97,96,97,96));
            Explosion.AddFrame(new IntRect(194,96,97,96));
            Explosion.AddFrame(new IntRect(291,96,97,96));
            Explosion.AddFrame(new IntRect(388,96,97,96));
            Explosion.AddFrame(new IntRect(0,192,97,96));
            Explosion.AddFrame(new IntRect(97,192,97,96));
            Explosion.AddFrame(new IntRect(194,192,97,96));
            Explosion.AddFrame(new IntRect(291,192,97,96)); 
            Explosion.AddFrame(new IntRect(388,192,97,96));
        }

        public override void update()
        {
            if(!_interact)
                Position += _direction;
            s.Position = Position;

            if (_interact)
            {
                _explodingSprite.Position = Position;
                _explodingSprite.Update(Globals.FRAME_TIME);
                _explodingSprite.Play(Explosion);
            }
        }

        public override FloatRect getRect()
        {
            return s.GetGlobalBounds();
        }

        public override void draw()
        {
            if(!_interact)
                Controller.Window.Draw(s);
            if(_interact)
                Controller.Window.Draw(_explodingSprite);
        }

        public  Animation Explosion { set; get; }

        public override void OnImpact()
        {
            _interact = true;
        }
    }
}
