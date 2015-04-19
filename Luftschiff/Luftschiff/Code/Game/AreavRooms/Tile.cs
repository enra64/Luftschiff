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
        private Sprite _tileSprite;

        /// <summary>
        /// <para>constructor for the tile; decides what sprite will be used</para>
        /// 0:empty; 1:walls; 2:
        /// </summary>
        public Tile(int kind, Vector2f position, Area.RoomTypes room)
        {
            //TODO: better sprites
            switch (kind)
            {
                case(0)://empty
                    _tileSprite = new Sprite(Globals.TileTextures[0]);
                    break;
                case(1)://wall
                    _tileSprite = new Sprite(Globals.TileTextures[1]);
                    break;
                case(2)://ground
                    _tileSprite = new Sprite(Globals.TileTextures[2]);
                    break;
                case(3)://special
                    switch (room)
                    {
                        case Area.RoomTypes.AirCannon:
                            _tileSprite = new Sprite(Globals.GunTexture);
                            break;
                        case Area.RoomTypes.AirEngine:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.AirHospital:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.AirLunch:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.Empty:
                            _tileSprite = new Sprite(Globals.TileTextures[2]);
                            break;
                        case Area.RoomTypes.GroundAirshipWorkshop:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.GroundBarracks:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.GroundMarketplace:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        case Area.RoomTypes.GroundTavern:
                            _tileSprite = new Sprite(Globals.TileTextures[3]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("room", room, null);
                    }
                    break;
                case(4):
                    //TODO add graphics for a door
                    break;
            }
            _tileSprite.Position = position;
        }

        /// <summary>
        /// does nothing yet
        /// </summary>
        public override void update()
        {}

        /// <summary>
        /// draws the tile
        /// </summary>
        public override void draw()
        {
            Controller.Window.Draw(_tileSprite);
        }

        /// <summary>
        /// overridden in tile to return the static sprite bounds
        /// </summary>
        /// <returns></returns>
        public override FloatRect getRect()
        {
            return _tileSprite.GetGlobalBounds();
        }
    }
}
