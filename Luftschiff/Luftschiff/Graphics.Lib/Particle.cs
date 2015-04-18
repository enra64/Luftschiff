using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class Particle : Drawable
    {
        private static readonly Random _randomizer = new Random();
        private static readonly object syncLock = new object();
        private static Clock _lifeClock;
        private readonly Shape _circle;

        public Particle(Time aliveTime, float radius, Color color)
        {
            lock (syncLock)
            {
                LifeTime =
                    Time.FromSeconds(aliveTime.AsSeconds()*((_randomizer.Next(0, 3) + (float) _randomizer.NextDouble())));
            }
            _circle = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            _circle.Position = Position;
        }

        public Particle(Time aliveTime, float radius, Color color, Vector2f position)
        {
            lock (syncLock)
            {
                LifeTime =
                    Time.FromSeconds(aliveTime.AsSeconds() * ((_randomizer.Next(0, 3) + (float)_randomizer.NextDouble())));
            }
            _circle = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            Position = position;
            _circle.Position = Position;
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
        public Vector2f Position { get; set; }

        public void Update()
        {
            lock (syncLock)
            {
                _circle.Position = new Vector2f(_circle.Position.X + 5f * _randomizer.Next(0, 3), _circle.Position.Y + 5f * _randomizer.Next(0, 3));
            }
        }
    }
}