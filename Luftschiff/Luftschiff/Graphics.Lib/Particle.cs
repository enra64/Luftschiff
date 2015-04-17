using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class Particle
    {
        private Clock _clock; // Used to scale particle motion
        private Color _color;
        private char _dissolutionRate;
        private bool _dissolve; // Dissolution enabled?
        private Vector2f _gravity; // Affects particle velocities
        private Image _image; // See render() and remove()
        private List<Particle> _particleList;
        private float _particleSpeed; // Pixels per second (at most)
        private Vector2f _position; // Particle origin (pixel co-ordinates)
        private Random _randomizer; // Randomizes particle velocities
        private char _shape;
        private Sprite _sprite; // Connected to m_image
        private Color _transparent; // sf::Color( 0, 0, 0, 0 )
        private Vector2f _velocity;
    }
}