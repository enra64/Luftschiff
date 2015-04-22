using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Monsters;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    class FireBall : Projectile
    {
        //TODO Add a real class!
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public FireBall(Vector2f startPosition, Monster targetMonster) : base(targetMonster)
        {
            
        }

        public override bool ShouldKill { get; set; }

        public override void WhileImpacting()
        {
            throw new NotImplementedException();
        }
    }
}
