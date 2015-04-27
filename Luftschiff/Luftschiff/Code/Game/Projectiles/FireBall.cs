using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    class FireBall : KineticProjectile
    {
   
        public FireBall(ITarget target, ITarget startRoom, Texture projectileTexture) : base(target, startRoom, projectileTexture)
        {
        }

        public override bool ShouldKill { get; set; }

        public override void Update()
        {
            //move while impact has not happened
            if (!ImpactHappened)
                Position += Direction;
            else
            {
                ShouldKill = true;
            }
        }

        public override void WhileImpacting()
        {
        }

        public override void Draw() {
            //only draw when not interacting and if no damage has been made yet
            if (!ImpactHappened && !HasMadeDamage)
                Controller.Window.Draw(Sprite);
        }
    }
}
