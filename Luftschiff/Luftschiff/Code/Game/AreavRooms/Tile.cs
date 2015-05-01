using SFML.Graphics;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Luftschiff.Code.Game.AreavRooms
{
    internal class Tile : Object
    {
        public const int TILE_FLOOR = 0;
        public const int TILE_EMPTY = 1;
        public const int TILE_WALL = 2;
        public const int TILE_SPECIAL = 3;
        public const int TILE_DOOR = 4;

        public Tile(int kind, Vector2f position, Area.RoomTypes room)
        {
            switch (kind)
            {
                case (TILE_FLOOR): //floor
                case (TILE_EMPTY): //TODO: emptyness
                    Sprite = new Sprite(Globals.TileFloor);
                    break;
                case (TILE_WALL): //walls
                    //TODO add graphics for walls
                    Sprite = new Sprite(Globals.TileElWall);
                    Sprite.Scale = new Vector2f(0.163f,0.163f);
                    break;
                case (TILE_SPECIAL):
                    switch (room)
                    {
                        //removed every case that is not handled different
                        case Area.RoomTypes.AirCannon:
                            //cannon room has advanced sprite, use normal ground for that
                            Sprite = new Sprite(Globals.TileFloor);
                            break;
                        default:
                            Sprite = new Sprite(Globals.TileMetall);
                            Sprite.Scale = new Vector2f(0.163f,0.163f);
                            break;
                    }
                    break;
                case (TILE_DOOR):
                    //TODO add graphics for a door
                    Sprite = new Sprite(Globals.TileDoor);
                    Sprite.Scale = new Vector2f(0.25f, .25f);                    
                    break;
            }
            Sprite.Position = position;
        }
    }
}