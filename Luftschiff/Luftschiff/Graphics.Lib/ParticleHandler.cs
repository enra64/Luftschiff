﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class ParticleHandler
    {
        private static RenderWindow _win;
        private static List<ShapeParticle> _particleKeeper;

        public ParticleHandler(int number, Color color)
        {
            _particleKeeper = new List<ShapeParticle>(number);
            for (var i = 1; i <= number; i++)
            {
                var par = new Particles.GenericCircles(Time.FromSeconds(3f), 10f, color);
                Add(par);
            }
            
            _win = Controller.Window;
        }

        private static void Add(ShapeParticle shapeParticle)
        {
            _particleKeeper.Add(shapeParticle);
        }

        private static void Remove()
        {
            _particleKeeper.RemoveAll(par => par.LifeTime <= par.LifeClock.ElapsedTime);
        }

        public void Update()
        {
            Console.WriteLine("Current Particles: "+ _particleKeeper.Count);
            foreach (var listedParticle in _particleKeeper)
            {
                listedParticle.Update();
            }
            if (MouseHandler.UnhandledClick)
            {
                var par = new Particles.GenericCircles(Time.FromSeconds(3f), 10f, Color.Yellow, MouseHandler.LastClickPosition);
                MouseHandler.UnhandledClick = false;
                Add(par);
            }
            Remove();
            
        }

        public void Draw()
        {
            foreach (var listedParticle in _particleKeeper)
            {
                _win.Draw(listedParticle);
            }
        }
    }
}