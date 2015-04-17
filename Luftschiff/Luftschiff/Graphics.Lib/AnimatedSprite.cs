using System;
using Luftschiff.Code;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Graphics.Lib
{
    internal class AnimatedSprite : Transformable, Drawable
    {
        private readonly Vertex[] _vertices;
        private Animation _animation;
        private int _currentFrame;
        private Time _currentTime;
        private Time _deltaTime;
        private Time _frameTime;
        private bool _isPaused;
        private Texture _texture;
        private bool _textureLoaded;
        private bool _validAnimation;

        public AnimatedSprite(Time frameTime, bool paused, bool looped, Vector2f position)
        {
            _animation = null;
            _frameTime = frameTime;
            _currentFrame = 0;
            _isPaused = paused;
            Looped = looped;
            _texture = null;
            _vertices = new Vertex[4];
            Position = position;
            SetColor(new Color(255, 255, 255, 255));
        }

        public Time FrameTime
        {
            set { _frameTime = value; }
            get { return _frameTime; }
        }

        public bool Looped { get; set; }

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

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (_validAnimation && _textureLoaded)
            {
                states.Transform = Transform;
                states.Texture = _texture;
                target.Draw(_vertices, 0, 4, PrimitiveType.Quads, states);
            }
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

        public void SetColor(Color color)
        {
            _vertices[0].Color = color;
            _vertices[1].Color = color;
            _vertices[2].Color = color;
            _vertices[3].Color = color;
        }

        public FloatRect GetLocalBounds()
        {
            var rect = _animation.GetFrame(_currentFrame);
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
            _deltaTime = Globals.FRAME_TIME;
            if (_validAnimation)
            {
                var rect = _animation.GetFrame(newFrame);
                _vertices[0].Position = new Vector2f(0f, 0f);
                _vertices[1].Position = new Vector2f(0f, rect.Height);
                _vertices[2].Position = new Vector2f(rect.Width, rect.Height);
                _vertices[3].Position = new Vector2f(rect.Width, 0f);

                var left = rect.Left + 0.0001f;
                var rigth = left + rect.Width;
                float top = rect.Top;
                var bottom = top + rect.Height;

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

        public void Update(Time delta)
        {
            _deltaTime = delta;
            //Console.WriteLine(_validAnimation + " " + _textureLoaded + " " + _isPaused + " " + Looped);
            //unpaused and valid anim
            if (!_isPaused && _validAnimation)
            {
                //add deltatime
                _currentTime += _deltaTime;

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

                        if (!Looped)
                        {
                            _isPaused = true;
                        }
                    }

                    // set the current frame without time reset
                    SetFrame(_currentFrame, false);
                }
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