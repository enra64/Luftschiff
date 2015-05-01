﻿using System;
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
            IntegerTilemap = LoadStandardTilekinds(2);
            initializeTilemap(Area.RoomTypes.AirHospital);

            var wardSprite = new Sprite(Globals.HospitalTexture);

            //guess position by using the position of the tiles
            wardSprite.Position = ObjectTilemap[1, 1].Position;
            wardSprite.Scale = new Vector2f(.5f,.5f);

            //add to sprite list so it gets drawn automatically
            AdditionalRoomSprites.Add(wardSprite);
        }
        public void Crewhealing()
        {
            
            if (CrewList.Count >= 2)
            {
                //look at every crewmember in room 
                for (int i = 0; i < CrewList.Count; i++)
                {
                    // heal by a maximum of 10 health points
                    for (int k = 0; k < 10 && CrewList.ElementAt(i)._health < 100; k++)
                        Console.WriteLine("healed");
                        CrewList.ElementAt(i)._health++;
                }
            } 
        }

    }
}
