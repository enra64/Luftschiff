using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Weapons;

namespace Luftschiff.Code.Global
{
    class Collider
    {
        private List<Monster> _monsterList;
        private List<Weapons> _weaponList;

        public Collider(List<Monster> monster, List<Weapons> weapons)
        {
            _monsterList = monster;
            _weaponList = weapons;
        }

        public void Update()
        {
            foreach (var m in _monsterList)
            {
                foreach (var w in _weaponList)
                {
                    if (w.Position.Y - 10f >= m.Position.Y) 
                        w.Interact();
                }
            }
        }

    }
}
