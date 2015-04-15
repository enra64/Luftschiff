using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using SFML;
using SFML.Graphics;
using SFML.Window;

namespace SoloGame
{
    class AnimatedSprite: SFML.Graphics.Transformable, SFML.Graphics.Drawable
    {
        private Animation m_animation;
        TimeSpan m_frameTime;
        TimeSpan m_currentTime;
        private int m_currentFrame;
        private bool m_isLooped;
        private bool m_isPaused;
        private bool m_validAnimation;
        private bool m_textureLoaded;
        private Texture m_texture;
        private Vertex[] m_vertices = new Vertex[4];
        private Transform m_transform;

        public AnimatedSprite(TimeSpan frameTime ,bool paused , bool looped)
        {
           m_frameTime = frameTime;
           m_currentFrame= 0;
           m_isPaused = paused;
           m_isLooped = looped;
        }

        public void setAnimation(Animation animation)
        {
            m_animation = animation;
            m_validAnimation = true;
            m_texture = m_animation.Spritesheet;
            m_textureLoaded = true;
            m_currentFrame = 0;
            setFrame(m_currentFrame, false);
        }

        public void setFrameTime(TimeSpan time)
        {
            m_frameTime = time;
        }

        public void play()
        {
            m_isPaused = false;
        }

        public void play(Animation animation)
        {
            if(Animation != animation)
                setAnimation(animation);

            play();
        }
        public void pause()
        {
            m_isPaused = true;
        }

        public void stop()
        {
            m_isPaused = true;
            m_currentFrame = 0;
            setFrame(m_currentFrame, true);
        }
        public bool isLooped
        {
            get{return m_isLooped;}
            set{m_isLooped = value;}
        }

        void setColor(Color color)
        {
            m_vertices[0].Color = color;
            m_vertices[1].Color = color;
            m_vertices[2].Color = color;
            m_vertices[3].Color = color;
        }

        public Animation Animation
        {
            get {return m_animation;}
            set {}
        }

        public FloatRect getLocalBounds()
        {
            IntRect rect = m_animation.getFrame(m_currentFrame);

            float width = (float)Math.Abs(rect.Width);
            float heigth = (float)Math.Abs(rect.Height);

            return new FloatRect(0f,0f,width,heigth);
        }

        public FloatRect getGlobalBounds()
        {
            return m_transform.TransformRect(getLocalBounds());
        }
        public bool isPlaying()
        {
            return !m_isPaused;
        }

        public TimeSpan getFrameTime()
        {
            return m_frameTime;
        }

        public void setFrame(int newFrame, bool resetTime)
        {
            if (m_validAnimation)
            {
                IntRect rect = m_animation.getFrame(newFrame);
                m_vertices[0].Position = new Vector2f(0f, 0f);
                m_vertices[1].Position = new Vector2f(0f, (float)(rect.Height));
                m_vertices[2].Position = new Vector2f((float)(rect.Width), (float)(rect.Height));
                m_vertices[3].Position = new Vector2f((float)(rect.Width), 0f);

                float left = (float)(rect.Left) + 0.0001f;
                float rigth = left + (float)(rect.Width);
                float top = (float)(rect.Top);
                float bottom = top + (float)(rect.Height);

                m_vertices[0].TexCoords = new Vector2f(left, top);
                m_vertices[0].TexCoords = new Vector2f(left, bottom);
                m_vertices[0].TexCoords = new Vector2f(rigth, bottom);
                m_vertices[0].TexCoords = new Vector2f(rigth, top);
            }

            if (resetTime)
            {
                m_currentTime = TimeSpan.Zero;
            }
        }

        public void update(TimeSpan deltaTime)
        {
            //unpaused and valid anim
            if (!m_isPaused && m_validAnimation)
            {
                //add deltatime
                m_currentTime += deltaTime;

                // if current time is bigger then the frame time advance one frame
                if (m_currentTime >= m_frameTime)
                {
                    m_currentTime = new TimeSpan(m_currentTime.Ticks % m_frameTime.Ticks);

                    //next animation
                    if (m_currentFrame + 1 < m_animation.getSize())
                        m_currentFrame++;

                    else
                    {
                        // animation has ended
                        m_currentFrame = 0; // reset to start

                        if (!m_isLooped)
                        {
                            m_isPaused = true;
                        }
                    }

                    // set the current frame without time reset
                    setFrame(m_currentFrame, false);
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (m_validAnimation && m_textureLoaded)
            {
                states.Transform *= states.Transform;
                states.Texture = m_texture;
                target.Draw(m_vertices, 0, 4, PrimitiveType.Quads, states);
            }
        }
    }
}
