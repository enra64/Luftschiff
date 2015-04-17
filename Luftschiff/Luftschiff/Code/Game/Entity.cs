using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using System;

namespace Luftschiff.Code.Game
{
    abstract class Entity
    {

        Vector2f position;
        Vector2f scale; // lets see if it is not senseless 
        AnimatedSprite sprite;

        /// <summary>
        /// get rectangle for collision
        /// is inherited by the rectangle of the sprite
        /// </summary>
        /// <returns></returns>
        public virtual FloatRect getRect()
        {
            return sprite.GetGlobalBounds();
        }

        /// <summary>
        /// this checks whether the sprite rectangle contains the click position
        /// </summary>
        public virtual Boolean CheckClick(Vector2f clickPosition)
        {
            return getRect().Contains(clickPosition.X, clickPosition.Y);
        }

        // update function : using in ingame Action
        //changed to "abstract" to force implementation
        public abstract void update();

        // draw sprites 
        public virtual void draw()
        {
            Controller.Window.Draw(sprite);
        }

        public virtual String getSave()
        {
            return "";
        }
    }
}
