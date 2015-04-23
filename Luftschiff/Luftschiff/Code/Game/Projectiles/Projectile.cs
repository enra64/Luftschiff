using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game.Projectiles
{
    /// <summary>
    ///     Empty class for Collidermanagment
    /// </summary>
    internal abstract class Projectile : Object
    {
        /// <summary>
        ///     Whether or not the projectile has already inflicted its damage upon the monster
        /// </summary>
        public bool HasMadeDamage;

        /// <summary>
        ///     whether or not the WhileImpacting has already been called
        /// </summary>
        public bool ImpactHappened;

        /// <summary>
        ///     Monster the projectile is aimed at
        /// </summary>
        public readonly ITarget Target;

        /// <summary>
        ///     Set the target monster and init ImpactHappened, HasMadeDamage to false
        /// </summary>
        /// <param name="target">The target monster</param>
        public Projectile(ITarget target)
        {
            Target = target;
            ImpactHappened = false;
            HasMadeDamage = false;
        }

        /// <summary>
        ///     Whether the collider should kill this projectile. enables blocking for traveling projectiles
        /// </summary>
        public abstract bool ShouldKill { get; set; }

        /// <summary>
        ///     Force Projectiles to have an update - Update function...
        /// </summary>
        public abstract void Update();

        /// <summary>
        ///     Executed on impact of the Projectile
        /// </summary>
        public abstract void WhileImpacting();
    }
}