using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class AnimatedSprite : Transformable , Drawable 
    {
        private Animation _animation;
        private Time _frameTime;
        private Time _currentTime;
        private int _currentFrame;
        private bool _isLooped;
        private bool _isPaused;
        private bool _validAnimation;
        private bool _textureLoaded;
        private Texture _texture;
        private Vertex[] _vertices;

        public AnimatedSprite(Time frameTime, bool paused, bool looped)
        {
            _animation = null;
            _frameTime = frameTime;
            _currentFrame = 0;
            _isPaused = paused;
            _isLooped = looped;
            _texture = null;
            _vertices = new Vertex[4];
        }

        public Time FrameTime
        {
            set { _frameTime = value; }
            get { return _frameTime; }
        }

        public void Play(Animation animation)
        {
            if (Animation != animation)
                Animation = animation;
            Play();
        }

        public void Play()
        {
            _isPaused = false;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Stop()
        {
            _isPaused = true;
            _currentFrame = 0;
            SetFrame(_currentFrame, true);
        }

        public bool Looped
        {
            get { return _isLooped; }
            set { _isLooped = value; }
        }

        public void SetColor(Color color)
        {
            _vertices[0].Color = color;
            _vertices[1].Color = color;
            _vertices[2].Color = color;
            _vertices[3].Color = color;
        }

        public Animation Animation
        {
            set
            {
                _animation = value;
                _validAnimation = true;
                _texture = _animation.Texture;
                _textureLoaded = true;
                _currentFrame = 0;
                SetFrame(_currentFrame, false);
            }
            get { return _animation; }
        }

        public FloatRect GetLocalBounds()
        {
            IntRect rect = _animation.GetFrame(_currentFrame);

            float width = Math.Abs(rect.Width);
            float heigth = Math.Abs(rect.Height);

            return new FloatRect(0f, 0f, width, heigth);
        }

        public FloatRect GetGlobalBounds()
        {
            return Transform.TransformRect(GetLocalBounds());
        }

        public bool IsPlaying()
        {
            return !_isPaused;
        }

        public void SetFrame(int newFrame, bool resetTime)
        {
            if (_validAnimation)
            {
                IntRect rect = _animation.GetFrame(newFrame);
                SetColor(new Color(255,255,255,255));
                if (_vertices[1].Color == new Color(0, 0, 0, 0))
                    throw new Exception("Color not configured!");
                _vertices[0].Position = new Vector2f(0f, 0f);
                _vertices[1].Position = new Vector2f(0f, rect.Height);
                _vertices[2].Position = new Vector2f(rect.Width, rect.Height);
                _vertices[3].Position = new Vector2f(rect.Width, 0f);

                float left = rect.Left + 0.0001f;
                float rigth = left + rect.Width;
                float top = rect.Top;
                float bottom = top + rect.Height;

                _vertices[0].TexCoords = new Vector2f(left, top);
                _vertices[1].TexCoords = new Vector2f(left, bottom);
                _vertices[2].TexCoords = new Vector2f(rigth, bottom);
                _vertices[3].TexCoords = new Vector2f(rigth, top);
            }

            if (resetTime)
            {
                _currentTime = Time.Zero;
            }
        }

        public void Update(Time deltaTime)
        {
            //unpaused and valid anim
            if (!_isPaused && _validAnimation)
            {
                //add deltatime
                _currentTime += deltaTime;

                // if current time is bigger then the frame time advance one frame
                if (_currentTime >= _frameTime)
                {
                    _currentTime = Time.FromMilliseconds((_currentTime.AsMilliseconds()%_frameTime.AsMilliseconds()));

                    //next animation
                    if (_currentFrame + 1 < _animation.GetSize())
                        _currentFrame++;

                    else
                    {
                        // animation has ended
                        _currentFrame = 0; // reset to start

                        if (!_isLooped)
                        {
                            _isPaused = true;
                        }
                    }

                    // set the current frame without time reset
                    SetFrame(_currentFrame, false);
                }
            }
        }

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (_validAnimation && _textureLoaded)
            {
                states.Transform = Transform;
                states.Texture = _texture;
                target.Draw(_vertices,0,4, PrimitiveType.Quads, states);
            }
        }

        public void Move(Vector2f offset)
        {

            {
                Position = new Vector2f(Position.X + offset.X, Position.Y + offset.Y);
            }
        }

        public void Move(float offsetX, float offsetY)
        {
            Position = new Vector2f(Position.X + offsetX, Position.Y + offsetY);
        }
    }
}