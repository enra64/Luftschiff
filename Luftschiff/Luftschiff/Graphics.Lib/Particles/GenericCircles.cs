﻿using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib.Particles
{
    internal class GenericCircles : ShapeParticle
    {
        private static readonly Random _randomizer = new Random();
        private static readonly object syncLock = new object();
        private static Clock _lifeClock;

        public GenericCircles(Time aliveTime, float radius, Color color)
        {
            lock (syncLock)
            {
                LifeTime =
                    Time.FromSeconds(aliveTime.AsSeconds()*((_randomizer.Next(0, 3) + (float) _randomizer.NextDouble())));
            }
            Shape = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            Shape.Position = Position;
        }

        public GenericCircles(Time aliveTime, float radius, Color color, Vector2f position)
        {
            lock (syncLock)
            {
                LifeTime =
                    Time.FromSeconds(aliveTime.AsSeconds()*((_randomizer.Next(0, 3) + (float) _randomizer.NextDouble())));
            }
            Shape = new CircleShape(radius);
            LifeClock = new Clock();
            Color = color;
            Position = position;
            Shape.Position = Position;
        }

        public override Time LifeTime { get; set; }

        public override Clock LifeClock
        {
            get { return _lifeClock; }
            set { _lifeClock = value; }
        }

        public override Color Color { get; set; }
        public override Vector2f Position { get; set; }
        public override Shape Shape { get; set; }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            Shape.FillColor = Color;
            Shape.OutlineColor = new Color(150, 150, 150, 255);
            Shape.OutlineThickness = 5f;
            Shape.Draw(target, states);
        }

        public override void Update()
        {
        }

        public override void Move(Vector2f moveVector2F)
        {
            Shape.Position = moveVector2F;
        }
    }
}