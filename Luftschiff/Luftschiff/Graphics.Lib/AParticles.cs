using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    abstract class AParticles: Drawable
    {
        public abstract Color Color { get; set; }
        public abstract Clock LifeClock { get; set; }
        public abstract Time LifeTime { get; set; }
        public abstract Vector2f Position { get; set; }
        public abstract void Move(Vector2f moveVector2F);
        public abstract void Update();
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
