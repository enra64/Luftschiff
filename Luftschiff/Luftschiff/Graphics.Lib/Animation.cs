using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Window;
using SFML.Graphics;

namespace Luftschiff.Graphics.Lib
{
    class Animation
    {
        private List<IntRect> m_frames = new List<IntRect>();
        private Texture m_texture;

        public Animation()
        {

        }

        public void addFrame(IntRect rect)
        {
            m_frames.Add(rect);
        }

        public IntRect getFrame(int n)
        {
            return m_frames[n];
        }

        public Texture Spritesheet
        {
            get { return m_texture; }
            set { m_texture = value; }
            
        }

        public int getSize()
        {
            return m_frames.Count;
        }
    }
}
