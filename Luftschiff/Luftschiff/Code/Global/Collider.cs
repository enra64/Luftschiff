using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Game.Weapons;
using SFML.Audio;

namespace Luftschiff.Code.Global
{
    static class Collider
    {
        private static List<Monster> _monsterList = new List<Monster>();
        private static List<Projectile> _projectileList = new List<Projectile>();

        public static void Update()
        {
            /*
            foreach (var m in _monsterList)
            {
                foreach (var w in _projectileList)
                {
                    if (w.Position.X - 5f >= m.Position.X)
                    {
                        w.WhileImpacting();
                    }
                }
            }*/
            foreach(var w in _projectileList)
                if (w.Position.X >= w.TargetMonster.Position.X)
                {
                    //if first call after impact, induce damage to monster
                    if(!w.HasMadeDamage)
                        w.TargetMonster.ReceiveDamageByShip(0, true);
                    //damage has now been done
                    w.HasMadeDamage = true;
                    //call the WhileImpacting to signal the projectile that the impact happened
                    w.WhileImpacting();
                    //impact has occured
                    w.ImpactHappened = true;
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
