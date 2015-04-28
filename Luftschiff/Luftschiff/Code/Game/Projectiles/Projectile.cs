using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.Game.Projectiles
{
    /// <summary>
    ///     Empty class for Collidermanagment
    /// </summary>
    internal abstract class Projectile : Object
    {

        /// <summary>
        ///     whether or not the OnImpact has already been called / the whileovertarget is being called
        /// </summary>
        public bool ImpactHappened;

        /// <summary>
        ///     Target the projectile is aimed at
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
        ///     Executed as long as the projectile is over its target
        /// </summary>
        public abstract void WhileOverTarget();

        /// <summary>
        ///     Called _once_ after the projectile hit
        /// </summary>
        public abstract void OnImpact();
    }
}