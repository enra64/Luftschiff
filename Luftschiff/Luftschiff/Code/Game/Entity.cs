using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game
{
    internal abstract class Entity
    {
        private Vector2f _position;
        public Vector2f Scale; // lets see if it is not senseless 
        public AnimatedSprite Sprite;

        /// <summary>
        ///     if the standard entity sprite is used, this will return its position;
        ///     else, a distinct position variable is used
        /// </summary>
        public virtual Vector2f Position
        {
            get { return Sprite != null ? Sprite.Position : _position; }
            set
            {
                if (Sprite != null)
                    Sprite.Position = value;
                else
                    _position = value;
            }
        }

        public Vector2f Center
        {
            get { return new Vector2f(Position.X + getRect().Width/2, Position.Y + getRect().Height/2); }
        }

        /// <summary>
        ///     get rectangle for collision
        ///     is inherited by the rectangle of the sprite
        /// </summary>
        public virtual FloatRect getRect()
        {
            return Sprite.GetGlobalBounds();
        }

        /// <summary>
        ///     this checks whether the sprite rectangle contains the position
        /// </summary>
        public virtual bool IsClickInside(Vector2f clickPosition)
        {
            return getRect().Contains(clickPosition.X, clickPosition.Y);
        }

        // update function : using in ingame Action
        //changed to "abstract" to force implementation
        public abstract void Update();

        public virtual void Draw()
        {
            Controller.Window.Draw(Sprite);
        }

        public virtual string getSave()
        {
            return "";
        }
    }
}