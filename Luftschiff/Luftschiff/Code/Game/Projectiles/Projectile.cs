using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Graphics.Lib;

namespace Luftschiff.Code.Game.Projectiles
{
    /// <summary>
    /// Empty class for Collidermanagment
    /// </summary>
    abstract class Projectile : Object
    {
        /// <summary>
        /// Force Projectiles to have an update - Update function...
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Executed on impact of the Projectile
        /// </summary>
        public abstract void OnImpact();
    }
}
