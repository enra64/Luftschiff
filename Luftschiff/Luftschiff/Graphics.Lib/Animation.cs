
using System.Collections.Generic;
using SFML.Graphics;

namespace Luftschiff.Graphics.Lib
{
    class Animation
    {
        private readonly List<IntRect> _frames = new List<IntRect>();
        private Texture _texture;

        public void AddFrame(IntRect rect)
        {
            _frames.Add(rect);
        }

        public IntRect GetFrame(int n)
        {
            return _frames[n];
        }

        public Texture Texture
        {
            set { _texture = value; }
            get { return _texture;}
        }

        public int GetSize()
        {
            return _frames.Count;
        }
    }
}
