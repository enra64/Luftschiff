using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class Particle
    {
        private Clock _clock; 
        private Color _color;
        private char _dissolutionRate;
        private bool _dissolve;
        private Vector2f _gravity; 
        private Image _image;
        private List<Particle> _particleList;
        private float _particleSpeed;
        private Vector2f _position; 
        private Random _randomizer; 
        private char _shape;
        private Sprite _sprite;
        private Color _transparent; 
        private Vector2f _velocity;
    }
}