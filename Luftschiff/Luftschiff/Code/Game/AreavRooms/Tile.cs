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
                    switch (room)
                    {
                        case Area.RoomTypes.AirCannon:
                            s = new Sprite(Globals.GunTexture);
                            break;
                        case Area.RoomTypes.AirEngine:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.AirHospital:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.AirLunch:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.Empty:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.GroundAirshipWorkshop:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.GroundBarracks:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.GroundMarketplace:
                            s = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.GroundTavern:
                            s = new Sprite(Globals.TileTextures[2]);
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
