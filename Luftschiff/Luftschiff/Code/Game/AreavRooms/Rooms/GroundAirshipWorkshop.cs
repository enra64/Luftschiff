using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class GroundAirshipWorkshop : Room
    {
        public GroundAirshipWorkshop(Vector2f position) : base(position)
        {
        }

        public override void FinalizeTiles()
        {
            //AddDoorsToTileArray();
            //initializeTilemap(Area.RoomTypes.AirHospital);
        }
    }
}