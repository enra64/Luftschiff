using System.Collections.Generic;
using Luftschiff.Graphics.Lib.Particles;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class ParticleHandler
    {
        private static RenderWindow _win;
        private static List<AParticles> _particleKeeper;
        private readonly bool _dispersion;
        private float _dispx, _dispy;
        private AParticles _lastparticle;

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="number"></param>
        /// <param name="color"></param>
        public ParticleHandler(int number, Color color, bool disp)
        {
            _particleKeeper = new List<AParticles>(number);
            for (var i = 1; i <= number; i++)
            {
                var par = new GenericCircles(Time.FromSeconds(3f), 10f, color);
                Add(par);
            }
            _dispersion = disp;
            _win = Controller.Window;
            _dispx = 0;
            _dispy = 0;
            _lastparticle = new GenericCircles(Time.FromSeconds(0), 0, new Color(0, 0, 0, 0));
        }

        /// <summary>
        ///     add a particle to the handler
        /// </summary>
        /// <param name="shapeParticle"></param>
        private static void Add(ShapeParticle shapeParticle)
        {
            _particleKeeper.Add(shapeParticle);
        }

        /// <summary>
        ///     removes particles that passed their lifetime
        /// </summary>
        private static void Remove()
        {
            _particleKeeper.RemoveAll(par => par.LifeTime <= par.LifeClock.ElapsedTime);
        }

        /// <summary>
        ///     update loop
        /// </summary>
        public void Update()
        {
            if (!_dispersion)
            {
                foreach (var listedParticle in _particleKeeper)
                {
                    listedParticle.Update();
                }
            }
            else
            {
                foreach (var listedParticle in _particleKeeper)
                {
                    listedParticle.Move(new Vector2f(_dispx, _dispy));
                    _dispy = 360 - _lastparticle.Position.Y + 1;
                    _dispx = 360 - _lastparticle.Position.X + 1;
                    _lastparticle = listedParticle;
                }
            }
            if (MouseHandler.UnhandledClick)
            {
                var par = new GenericCircles(Time.FromSeconds(3f), 10f, Color.Yellow, MouseHandler.LastClickPosition);
                MouseHandler.UnhandledClick = false;
                Add(par);
            }
            Remove();
        }

        /// <summary>
        ///     draw loop
        /// </summary>
        public void Draw()
        {
            foreach (var listedParticle in _particleKeeper)
            {
                _win.Draw(listedParticle);
            }
        }
    }
}