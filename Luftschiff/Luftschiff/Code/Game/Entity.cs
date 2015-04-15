using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Game
{
    abstract class Entity
    {

        Vector2f position;
        Vector2f scale; // lets see if it is not senseless 
        AnimatedSprite sprite;

        // get Rectangle for collision 
        public FloatRect getRect()
        {
            return sprite.getLocalBounds();
        }
        // update function : using in ingame Action
        abstract public void update() { }

        // draw sprites 
        public void draw()
        {
            Controller.Window.Draw(sprite);
        }

        //public String getSave(){}
    }
}
