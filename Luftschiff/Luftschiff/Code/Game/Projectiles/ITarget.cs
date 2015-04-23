using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles {
    /// <summary>
    /// Interface for elements that can receive damage
    /// </summary>
    interface ITarget
    {
        /// <summary>
        /// The implementation should damage its element somehow.
        /// </summary>
        /// <param name="damageAmount">How much damage, see damage model</param>
        void ReceiveDamage(int damageAmount);

        /// <summary>
        /// Implementation must determine whether an object has been hit by the projectile.
        /// </summary>
        /// <param name="projectilePosition">Current projectile position</param>
        /// <returns></returns>
        bool HasBeenHit(Vector2f projectilePosition);

        /// <summary>
        /// Center of the object. Any ideas whether that 
        /// </summary>
        /// <returns></returns>
        Vector2f Center { get; }
    }
}
