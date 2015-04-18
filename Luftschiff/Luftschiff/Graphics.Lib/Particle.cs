using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class Particle : Drawable
    {

        private static Clock _lifeClock;
        private readonly Shape _circle;

        public Particle(Time aliveTime, float radius, Color color)
        {
            ParticleHandler._randomizer = new Random();
            LifeTime = Time.FromSeconds(aliveTime.AsSeconds()*((ParticleHandler._randomizer.Next(0,3) + (float) ParticleHandler._randomizer.NextDouble())));
            _circle = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            _circle.Position = new Vector2f(100, 100);
        }

        public Time LifeTime { get; set; }

        public Clock LifeClock
        {
            get { return _lifeClock; }
            set { _lifeClock = value; }
        }

        public Color Color { get; set; }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _circle.FillColor = Color;
            _circle.Draw(target, states);
        }

        public void Update()
        {
            _circle.Position = new Vector2f(_circle.Position.X +5f * ParticleHandler._randomizer.Next(0,3), _circle.Position.Y + 5f* ParticleHandler._randomizer.Next(0,3));
        }
    }
}