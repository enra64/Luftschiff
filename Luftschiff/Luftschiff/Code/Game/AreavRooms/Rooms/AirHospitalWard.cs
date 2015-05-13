using System;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    internal class AirHospitalWard : Room
    {
        public AirHospitalWard(Vector2f position) : base(position)
        {
            IntegerTilemap = LoadStandardTilekinds(2);

            var wardSprite = new Sprite(Globals.HospitalTexture);

            //guess position by using the position of the tiles
            wardSprite.Position = ObjectTilemap[1, 1].Position;
            wardSprite.Scale = new Vector2f(.5f, .5f);

            //add to sprite list so it gets drawn automatically
            AdditionalRoomSprites.Add(wardSprite);
        }

        public override void FinalizeTiles()
        {
            AddDoorsToTileArray();
            initializeTilemap(Area.RoomTypes.AirHospital);
        }

        public void Crewhealing()
        {
            if (CrewList.Count >= 2)
            {
                //look at every crewmember in room 
                for (var i = 0; i < CrewList.Count; i++)
                {
                    //show notification
                    if (CrewList[i].Health < 100)
                        Notifications.Instance.AddNotification(Position, "HEALY HEALY");

                    var oldHealth = CrewList[i].Health;

                    // heal by a maximum of 10 health points
                    for (var k = 0; k < 10 && CrewList.ElementAt(i).Health < 100; k++)
                    {
                        //Console.WriteLine("healed");
                        CrewList.ElementAt(i).Health++;
                    }

                    if (oldHealth < 100)
                        Console.WriteLine("healed from " + oldHealth + " to " + CrewList[i].Health);
                }
            }
            // mousehandler update still there 
            //TODO check if this method is necessery
            Update();
        }
    }
}