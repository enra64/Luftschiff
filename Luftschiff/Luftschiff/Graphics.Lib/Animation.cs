using System.Collections.Generic;
using SFML.Graphics;

namespace Luftschiff.Graphics.Lib
{
    internal class Animation
    {
        private readonly List<IntRect> _frames = new List<IntRect>();

        public Animation(Texture texture)
        {
            Texture = texture;
        }

        public Texture Texture { set; get; }

        public void AddFrame(IntRect rect)
        {
            _frames.Add(rect);
        }

        public IntRect GetFrame(int n)
        {
            return _frames[n];
        }

        public int GetSize()
        {
            return _frames.Count;
        }
    }
}