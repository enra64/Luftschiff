using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    internal class Tile : Object
    {
        public Tile(int kind, Vector2f position, Area.RoomTypes room)
        {
            switch (kind)
            {
                case (0): //floor
                case (1): //TODO: emptyness
                    Sprite = new Sprite(Globals.TileFloor);
                    break;
                case (2): //walls
                    //TODO add graphics for walls
                    Sprite = new Sprite(Globals.TileWall);
                    break;
                
                case (3):
                    switch (room)
                    {
                        //removed every case that is not handled different
                        case Area.RoomTypes.AirCannon:
                            //cannon room has advanced sprite, use normal ground for that
                            Sprite = new Sprite(Globals.TileFloor);
                            break;
                        default:
                            Sprite = new Sprite(Globals.TileSpecial);
                            break;
                    }
                    break;
                case (4):
                    //TODO add graphics for a door
                    break;
            }
            Sprite.Position = position;
        }
    }
}