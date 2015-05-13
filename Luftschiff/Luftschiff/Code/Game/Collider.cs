using System.Collections.Generic;
using Luftschiff.Code.Game.Projectiles;

namespace Luftschiff.Code.Game
{
    internal class Collider
    {
        private readonly List<Projectile> _projectileList = new List<Projectile>();

        /// <summary>
        ///     The amount of projectiles tracked by the collider
        /// </summary>
        public int ProjectileCount
        {
            get { return _projectileList.Count; }
        }

        public void Update()
        {
            foreach (var projectile in _projectileList)
                //the projectiles target uses ITarget, and as such implements the hasbeenhit function
                if (projectile != null && projectile.Target.HasBeenHit(projectile.Center))
                {
                    //if first call after impact, induce damage to monster
                    if (!projectile.ImpactHappened)
                        projectile.Target.ReceiveDamage(100);
                    //call the WhileOverTarget to signal the projectile that the impact happened
                    projectile.WhileOverTarget();
                    //call OnImpact exactly once
                    if (!projectile.ImpactHappened)
                        projectile.OnImpact();
                    //impact has occured
                    projectile.ImpactHappened = true;
                }

            //remove all projectiles that flag to be killed
            _projectileList.RemoveAll(p => p.ShouldKill);

            //update all projectiles
            foreach (var p in _projectileList)
                p.Update();

            //remove null elements
            _projectileList.RemoveAll(s => s == null);
        }

        public void Draw()
        {
            foreach (var projectile in _projectileList)
            {
                projectile.Draw();
            }
        }

        public void AddProjectile(Projectile projectile)
        {
            _projectileList.Add(projectile);
        }
    }
}