using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class Particle : Sprite, Drawable
    {
        private static Random _randomizer;
        private static Clock _lifeClock;
        private readonly Shape _circle;
        private readonly Vector2f _position;

        public Particle(Time aliveTime, float radius, Color color)
        {
            _randomizer = new Random();
            LifeTime =
                Time.FromSeconds(aliveTime.AsSeconds()*(_randomizer.Next(0, 2) + (float) _randomizer.NextDouble()));
            _circle = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            _position = new Vector2f(100, 100);
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
            Position = _position;
        }
    }
}