using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game
{
    /// <summary>
    ///     A new "entity" type of on screen object which uses a standard sprite to enable easy drawing of static game content
    /// </summary>
    internal abstract class Object
    {
        /// <summary>
        ///     Contains the Objects Sprite.
        /// </summary>
        internal Sprite Sprite { get; set; }

        /// <summary>
        ///     Returns and sets the Position of this Objects Sprite
        /// </summary>
        public Vector2f Position
        {
            get { return Sprite.Position; }
            set { Sprite.Position = value; }
        }

        /// <summary>
        ///     Returns the Size of the Object with Scaling. When you write to this, it updates the Scale
        /// </summary>
        public Vector2f Size
        {
            get { return new Vector2f(Sprite.Texture.Size.X*Sprite.Scale.X, Sprite.Texture.Size.Y*Sprite.Scale.Y); }
            set { Sprite.Scale = new Vector2f(value.X/Sprite.Texture.Size.X, value.Y/Sprite.Texture.Size.Y); }
        }

        /// <summary>
        ///     Returns the global bounds of this object.
        /// </summary>
        public FloatRect Rect
        {
            get { return Sprite.GetGlobalBounds(); }
        }

        /// <summary>
        ///     Returns the center of this object
        /// </summary>
        public Vector2f Center
        {
            get { return new Vector2f(Position.X + Size.X/2, Position.Y + Size.Y/2); }
        }

        /// <summary>
        ///     Returns true if the click was inside the sprite of this object
        /// </summary>
        /// <param name="clickPosition">Position of the click.</param>
        /// <returns>Whether the object was clicked.</returns>
        public bool IsClickInside(Vector2f clickPosition)
        {
            return Rect.Contains(clickPosition.X, clickPosition.Y);
        }

        /// <summary>
        ///     Draws the sprite of this object.
        /// </summary>
        public virtual void Draw()
        {
            Controller.Window.Draw(Sprite);
        }
    }
}