using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class ParticleHandler
    {
        private static RenderWindow _win;
        private static List<Particle> _particleKeeper;

        public ParticleHandler(int number)
        {
            _particleKeeper = new List<Particle>(number);
            for (var i = 0; i <= number; i++)
            {
                var par = new Particle(Time.FromSeconds(10), 10f, Color.Magenta);
                Add(par);
            }
            
            _win = Controller.Window;
        }

        private static void Add(Particle particle)
        {
            _particleKeeper.Add(particle);
        }

        private static void Remove()
        {
            _particleKeeper.RemoveAll(par => par.LifeTime == par.LifeClock.ElapsedTime);
        }

        public void Update()
        {
            foreach (var listedParticle in _particleKeeper)
            {
                listedParticle.Update();
            }
            if (MouseHandler.UnhandledClick)
            {
                var par = new Particle(Time.FromSeconds(10), 10f, Color.Magenta);
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