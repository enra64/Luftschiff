﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class AirEngineRoom : Room
    {
        public AirEngineRoom(Vector2f position): base(position)
        {
            IntegerTilemap = LoadStandardTilekinds(2);
            initializeTilemap(Area.RoomTypes.AirEngine);

            //add additonal sprite: weapon
            var engineSprite = new Sprite(Globals.EngineTexture)
            {
                //guess position by using the position of the tiles
                Position = ObjectTilemap[1, 1].Position,
                
                //make it look okay
                Scale = new Vector2f(.5f, .5f)
            };

            //add to sprite list so it gets drawn automatically
            AdditionalRoomSprites.Add(engineSprite);
        }
    }
}
