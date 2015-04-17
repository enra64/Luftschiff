using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Tile : Entity
    {
        private Sprite s;
        public Tile(int kind, Vector2f position)
        {
            switch (kind)
            {
                case(0):
                    //TODO add graphics for emptyness
                    s = new Sprite(Globals.TileTextures[0]);
                    break;
                case(1):
                    //TODO add graphics for walls
                    s = new Sprite(Globals.TileTextures[3]);
                    break;
                case(2):
                    //TODO no idead which graphics
                    s = new Sprite(Globals.TileTextures[1]);
                    break;
                case(3):
                    //TODO stuff in the middle graphics
                    s = new Sprite(Globals.TileTextures[2]);
                    break;

            }
        }
        public override void update()
        {}

        public override void draw()
        {
            Controller.Window.Draw(s);
        }
    }
}
