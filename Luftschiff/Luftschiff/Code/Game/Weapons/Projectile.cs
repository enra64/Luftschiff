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
        private Vector2f _Direction;
        private Sprite s;

        public Projectile(Vector2f target, Vector2f startposition)
        {
            _Direction = (target - startposition) / 100;
            Position = startposition;
            s = new Sprite(Globals.CannonBallTexture);
        }

        public override void update()
        {
            Position += _Direction;
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
