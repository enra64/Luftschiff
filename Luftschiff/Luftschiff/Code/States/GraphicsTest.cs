﻿using System.Collections.Generic;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code.States
{
    internal class GraphicsTest : ProtoGameState
    {
        private static Animation _walkaround;
        private static Animation _turnaround;
        private static AnimatedSprite _movingSprite;
        private static RenderWindow _win;
        private static ParticleHandler _amazing;

        public GraphicsTest()
        {
            _win = Controller.Window;

            _walkaround = new Animation(new Texture("Assets/Graphics/rusty_sprites.png"));
            _walkaround.AddFrame(new IntRect(0, 0, 100, 100));
            _walkaround.AddFrame(new IntRect(0, 100, 100, 100));
            _walkaround.AddFrame(new IntRect(100, 100, 100, 100));
            _walkaround.AddFrame(new IntRect(100, 0, 100, 100));

            _turnaround = new Animation(new Texture("Assets/Graphics/rusty_sprites.png"));
            _turnaround.AddFrame(new IntRect(100, 0, 100, 100));
            _turnaround.AddFrame(new IntRect(100, 100, 100, 100));
            _turnaround.AddFrame(new IntRect(0, 100, 100, 100));
            _turnaround.AddFrame(new IntRect(0, 0, 100, 100));

            _movingSprite = new AnimatedSprite(Time.FromSeconds(0.5f), false, true, new Vector2f(0f, 0f));
            _amazing = new ParticleHandler(0, Color.Transparent, false);
        }

        /// <summary>
        ///     Main draw call for our Game.
        /// </summary>
        public override void draw()
        {
            _win.Clear();
            _win.Draw(_movingSprite);
            _amazing.Draw();
            _win.Display();
        }

        /// <summary>
        ///     This method gets called when the Gamestate is about to be stopped
        ///     to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill()
        {
        }

        /// <summary>
        ///     This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update()
        {
            _movingSprite.Update(Globals.FRAME_TIME);
            _movingSprite.Play(_walkaround);
            _amazing.Update();

            var stringList = new List<string>();
            stringList.Add("Blaue Particle!");
            stringList.Add("Rote Particle!");
            stringList.Add("Grüne Particle!");
            var particleList = new ListDialog(stringList, "Choose your particleflavor!", "Particles");
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (particleList.show() == 0)
                {
                    _amazing = new ParticleHandler(500, new Color(Color.Blue), true);
                }
                if (particleList.show() == 1)
                {
                    _amazing = new ParticleHandler(500, new Color(Color.Red), true);
                }
                if (particleList.show() == 2)
                {
                    _amazing = new ParticleHandler(500, new Color(Color.Green), true);
                }
            }
        }
    }
}