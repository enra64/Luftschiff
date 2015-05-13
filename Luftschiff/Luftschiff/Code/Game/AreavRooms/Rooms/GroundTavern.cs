using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class GroundTavern : Room
    {
        public GroundTavern(Vector2f position) : base(position)
        {
        }

        public override void FinalizeTiles()
        {
            //AddDoorsToTileArray();
            //initializeTilemap(Area.RoomTypes.AirHospital);
        }
    }
}