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
                    break;
                case(1):
                    break;
                case(2):
                    break;
                case(3):
                    break;

            }
        }
        public override void update()
        {
            throw new NotImplementedException();
        }
    }
}
