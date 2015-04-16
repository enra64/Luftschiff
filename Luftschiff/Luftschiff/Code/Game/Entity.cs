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
        public virtual void update()
        {
            Console.WriteLine("update not implemented");
        }

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
