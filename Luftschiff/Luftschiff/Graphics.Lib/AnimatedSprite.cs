using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    class AnimatedSprite: Transformable, Drawable
    {
        private Animation _animation;
        Time _frameTime;
        Time _currentTime;
        private int _currentFrame;
        private bool _isLooped;
        private bool _isPaused;
        private bool _validAnimation;
        private bool _textureLoaded;
        private Texture _texture;
        private readonly Vertex[] _vertices = new Vertex[4];
        private Transform _transform;

        public AnimatedSprite(Time frameTime ,bool paused , bool looped)
        {
           _frameTime = frameTime;
           _currentFrame= 0;
           _isPaused = paused;
           _isLooped = looped;
        }

        private void SetAnimation(Animation animation)
        {
            _animation = animation;
            _validAnimation = true;
            _texture = _animation.GetTexture();
            _textureLoaded = true;
            _currentFrame = 0;
            SetFrame(_currentFrame, false);
        }

        public void SetFrameTime(Time time)
        {
            _frameTime = time;
        }

        public void Play(Animation animation)
        {
            if(Animation != animation)
                SetAnimation(animation);
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
        public bool IsLooped
        {
            get{return _isLooped;}
            set{_isLooped = value;}
        }

        void SetColor(Color color)
        {
            _vertices[0].Color = color;
            _vertices[1].Color = color;
            _vertices[2].Color = color;
            _vertices[3].Color = color;
        }

        public Animation Animation
        {
            get {return _animation;}
        }

        public FloatRect GetLocalBounds()
        {
            IntRect rect = _animation.GetFrame(_currentFrame);

            float width = Math.Abs(rect.Width);
            float heigth = Math.Abs(rect.Height);

            return new FloatRect(0f,0f,width,heigth);
        }

        public FloatRect GetGlobalBounds()
        {
            return _transform.TransformRect(GetLocalBounds());
        }
        public bool IsPlaying()
        {
            return !_isPaused;
        }

        public Time GetFrameTime()
        {
            return _frameTime;
        }

        public void SetFrame(int newFrame, bool resetTime)
        {
            if (_validAnimation)
            {
                IntRect rect = _animation.GetFrame(newFrame);
                _vertices[0].Position = new Vector2f(0f, 0f);
                _vertices[1].Position = new Vector2f(0f, rect.Height);
                _vertices[2].Position = new Vector2f(rect.Width, rect.Height);
                _vertices[3].Position = new Vector2f(rect.Width, 0f);

                float left = rect.Left + 0.0001f;
                float rigth = rect.Width;
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
                    _currentTime = Time.FromMilliseconds((_currentTime.AsMilliseconds() % _frameTime.AsMilliseconds()));
                    
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

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_validAnimation && _textureLoaded)
            {
                states.Transform *= states.Transform;
                states.Texture = _texture;
                target.Draw(_vertices, 0, 4, PrimitiveType.Quads, states);
            }
        }
    }
}
