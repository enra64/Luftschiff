using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    class Tile : Entity
    {
        public Tile(int kind, Vector2f position)
        {
            switch (kind)
            {
                case(0):
                    //TODO add graphics for emptynes
                    break;
                case(1):
                    //TODO add graphics for walls
                    break;
                case(2):
                    //TODO no idead which graphics
                    break;
                case(3):
                    //TODO stuff in the middle graphics
                    break;

            }
        }
        public override void update()
        {
            throw new NotImplementedException();
        }
    }
}
