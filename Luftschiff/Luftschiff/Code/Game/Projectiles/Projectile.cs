using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Graphics.Lib;

namespace Luftschiff.Code.Game.Projectiles
{
    abstract class Projectile : Entity
    {
        //Empty class for Collidermanagment
        public override void update()
        {
        }

        public abstract void OnImpact();

    }
}
