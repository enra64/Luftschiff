using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using SFML.System;
using SFML.Graphics;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class AirHospitalWard : Room
    {
        public AirHospitalWard(Vector2f position) : base(position)
        {
            tilekind = loadStandardTilekinds(2);
            initializeTilemap(Area.RoomTypes.AirHospital);

            var wardSprite = new Sprite(Globals.HospitalTexture);

            //guess position by using the position of the tiles
            wardSprite.Position = _tilemap[1, 1].Position;
            wardSprite.Scale = new Vector2f(1.3f,1.3f);

            //add to sprite list so it gets drawn automatically
            _additionalRoomSprites.Add(wardSprite);
        }
        public override void Update()
        {
            // check if the room has to be repaired
            //TODO add other necessary function is they are needed 
            //if there are two or more persons in the hospital ward heal both by a maximum of 10 each round
            if (crewList.Count >= 2)
            {
                //look at every crewmember in room 
                for (int i = 0; i < crewList.Count; i++)
                {
                    // heal by a maximum of 10 health points

                    for (int k = 0; k < 10 && crewList.ElementAt(i)._health < 100; k++)

                        crewList.ElementAt(i)._health++;
                }
            }
            // mousehandler update still there 
            //TODO check if this method is necessery
            base.Update();
        }


    }
}
