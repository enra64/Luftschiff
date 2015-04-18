using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Weapons {
    class Projectile : Entity
    {
        private readonly Vector2f _direction;
        private readonly Sprite s;

        public Projectile(Vector2f target, Vector2f startposition)
        {
            Console.WriteLine("target position: " + target);
            _direction = (target - startposition) / 70;
            Position = startposition;
            s = new Sprite(Globals.CannonBallTexture);
        }

        public override void update()
        {
            Position += _direction;
            s.Position = Position;
        }

        public override FloatRect getRect()
        {
            return s.GetGlobalBounds();
        }

        public override void draw()
        {
            Controller.Window.Draw(s);
        }
    }
}
