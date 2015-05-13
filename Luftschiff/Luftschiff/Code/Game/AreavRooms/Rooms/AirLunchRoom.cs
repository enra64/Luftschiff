using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class AirLunchRoom : Room
    {
        public AirLunchRoom(Vector2f position)
            : base(position)
        {
            {
                IntegerTilemap = LoadStandardTilekinds(2);
            }
        }

        public override void FinalizeTiles()
        {
            AddDoorsToTileArray();
            initializeTilemap(Area.RoomTypes.AirLunch);
        }
    }
}