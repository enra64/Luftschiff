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

        /// <summary>
        /// New animatedsprite 
        /// Time: how long should anmation play?
        /// bool: paused animation?
        /// bool looped animation?
        /// position of animation
        /// </summary>
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

        /// <summary>
        /// time of 1 frame 
        /// </summary>
        public Time FrameTime
        {
            set { _frameTime = value; }
            get { return _frameTime; }
        }

        /// <summary>
        /// bool who controls a loop
        /// </summary>
        public bool Looped { get; set; }

        /// <summary>
        /// animation that defines the sprite
        /// </summary>
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
        /// <summary>
        /// Draw magic
        /// </summary>
        /// <param name="target"></param>
        /// <param name="states"></param>
        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (_validAnimation && _textureLoaded)
            {
                states.Transform = Transform;
                states.Texture = _texture;
                target.Draw(_vertices, 0, 4, PrimitiveType.Quads, states);
            }
        }

        /// <summary>
        /// Plays a specific animation and loads this animation
        /// </summary>
        public void Play(Animation animation)
        {
            if (Animation != animation)
                Animation = animation;
            Play();
        }

        /// <summary>
        /// unpauses a sprite
        /// </summary>
        public void Play()
        {
            _isPaused = false;
        }

        /// <summary>
        /// pauses a sprite
        /// </summary>
        public void Pause()
        {
            _isPaused = true;
        }

        /// <summary>
        /// stops a sprite, resets it too
        /// </summary>
        public void Stop()
        {
            _isPaused = true;
            _currentFrame = 0;
            SetFrame(_currentFrame, true);
        }

        /// <summary>
        /// sets sprite color
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            _vertices[0].Color = color;
            _vertices[1].Color = color;
            _vertices[2].Color = color;
            _vertices[3].Color = color;
        }

        /// <summary>
        /// Gives you the current frame size back
        /// </summary>
        public FloatRect GetLocalBounds()
        {
            var rect = _animation.GetFrame(_currentFrame);
            float width = Math.Abs(rect.Width);
            float heigth = Math.Abs(rect.Height);
            return new FloatRect(0f, 0f, width, heigth);
        }

        /// <summary>
        /// Gives you the global size back
        /// </summary>
        public FloatRect GetGlobalBounds()
        {
            return Transform.TransformRect(GetLocalBounds());
        }

        /// <summary>
        /// bool shows if sprite is playing
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying()
        {
            return !_isPaused;
        }

        /// <summary>
        /// Never use this please, plays a specific frame
        /// </summary>
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

        /// <summary>
        /// needs global.frame_time updates sprite through code magic
        /// </summary>
        /// <param name="delta"></param>
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
                    {
                        _currentFrame++;
                        TimesPlayed++;
                    }
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

        /// <summary>
        /// Moves sprite on vector
        /// </summary>
        public void Move(Vector2f offset)
        {

            {
                Position = new Vector2f(Position.X + offset.X, Position.Y + offset.Y);
            }
        }
        
        /// <summary>
        /// Mves sprite with offset coords
        /// </summary>
        public void Move(float offsetX, float offsetY)
        {
            Position = new Vector2f(Position.X + offsetX, Position.Y + offsetY);
        }

        //ERRORSOURCE: code mathmatics
        /// <summary>
        /// magic int us code magic and you might get the timeplayed value back
        /// </summary>
        public int TimesPlayed { get; set; }

    }
}