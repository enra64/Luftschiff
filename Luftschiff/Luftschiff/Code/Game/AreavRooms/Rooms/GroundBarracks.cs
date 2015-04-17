using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game.AreavRooms.Rooms
{
    class GroundBarracks : Room
    {
        public override void update()
        {
            throw new NotImplementedException();
        }
        public override void OnCrewArrive(CrewMember traveler) { }
        public override void inflictDamage(Monster monster, bool hits)
        {
            throw new NotImplementedException();
        }
    }
}
