using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Game.Weapons;

namespace Luftschiff.Code.Global
{
    static class Collider
    {
        private static List<Monster> _monsterList = new List<Monster>();
        private static List<Projectile> _projectileList = new List<Projectile>();

        public static void Update()
        {
            foreach (var m in _monsterList)
            {
                foreach (var w in _projectileList)
                {
                    if (w.Position.Y - 10f <= m.Position.Y) 
                        w.OnImpact();
                }
            }
        }

        public static void AddMonster(Monster monster)
        {
            _monsterList.Add(monster);
        }

        public static void AddProjectile(Projectile projectile)
        {
            _projectileList.Add(projectile);
        }

    }
}
