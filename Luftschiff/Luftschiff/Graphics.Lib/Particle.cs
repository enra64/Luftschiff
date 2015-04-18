using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal abstract class Particle : Drawable
    {
        public abstract Shape Shape { get; set; }

        public abstract Color Color { get; set; }
        public abstract Clock LifeClock { get; set; }
        public abstract Time LifeTime { get; set; }
        public abstract Vector2f Position { get; set; }

        public abstract void Update();

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}