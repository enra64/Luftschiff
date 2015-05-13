using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class EmptyRoom : Room
    {
        public EmptyRoom(Vector2f position)
            : base(position)
        {
            IntegerTilemap = LoadStandardTilekinds(3);
        }

        public override void FinalizeTiles()
        {
            AddDoorsToTileArray();
            initializeTilemap(Area.RoomTypes.Empty);
        }
    }
}