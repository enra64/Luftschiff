using System.Collections.Generic;
using Luftschiff.Code.Game.Projectiles;

namespace Luftschiff.Code.Global
{
    static class Collider
    {
        private static List<Projectile> _projectileList = new List<Projectile>();

        /// <summary>
        /// The amount of projectiles tracked by the collider
        /// </summary>
        public static int ProjectileCount { get { return _projectileList.Count; } }

        public static void Update()
        {
            foreach(var projectile in _projectileList)
                //the projectiles target uses ITarget, and as such implements the hasbeenhit function
                if (projectile.Target.HasBeenHit(projectile.Center))
                {
                    //if first call after impact, induce damage to monster
                    if(!projectile.HasMadeDamage)
                        projectile.Target.ReceiveDamage(100);
                    //damage has now been done
                    projectile.HasMadeDamage = true;
                    //call the WhileImpacting to signal the projectile that the impact happened
                    projectile.WhileImpacting();
                    //call OnImpact exactly once
                    if(!projectile.ImpactHappened)
                        projectile.OnImpact();
                    //impact has occured
                    projectile.ImpactHappened = true;
                }

            //remove all projectiles that flag to be killed
            _projectileList.RemoveAll(p => p.ShouldKill);
        }

        public static void AddProjectile(Projectile projectile)
        {
            _projectileList.Add(projectile);
        }

    }
}
