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
        public Tile(int kind, Vector2f position, Area.RoomTypes room)
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
                    switch (room)
                    {
                        case Area.RoomTypes.AirCannon:
                            break;
                        case Area.RoomTypes.AirEngine:
                            break;
                        case Area.RoomTypes.AirHospital:
                            break;
                        case Area.RoomTypes.AirLunch:
                            break;
                        case Area.RoomTypes.Empty:
                            break;
                        case Area.RoomTypes.GroundAirshipWorkshop:
                            break;
                        case Area.RoomTypes.GroundBarracks:
                            break;
                        case Area.RoomTypes.GroundMarketplace:
                            break;
                        case Area.RoomTypes.GroundTavern:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("room", room, null);
                    }
                    break;
                case(4):
                    //TODO add graphics for a door
                    break;
            }
            s.Position = position;
        }

        public override void update()
        {}

        public override void draw()
        {
            Controller.Window.Draw(s);
        }

        public override FloatRect getRect()
        {
            return s.GetGlobalBounds();
        }
    }
}
