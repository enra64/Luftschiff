using System.Collections.Generic;
using SFML.Graphics;

namespace Luftschiff.Graphics.Lib
{
    internal class Animation
    {
        private readonly List<IntRect> _frames = new List<IntRect>();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="texture"></param>
        public Animation(Texture texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// the texture which feeds the frames
        /// </summary>
        public Texture Texture { set; get; }

        /// <summary>
        /// Adds a frame as intrect
        /// </summary>
        public void AddFrame(IntRect rect)
        {
            _frames.Add(rect);
        }

        /// <summary>
        /// returnes the nth framerect
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IntRect GetFrame(int n)
        {
            return _frames[n];
        }

        /// <summary>
        /// returns the current number of loaded frames
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return _frames.Count;
        }
    }
}