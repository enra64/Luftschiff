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

        // get Rectangle for collision 
        public virtual FloatRect getRect()
        {
            return sprite.GetLocalBounds();
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
