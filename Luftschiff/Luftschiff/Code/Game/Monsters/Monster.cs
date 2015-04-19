using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Game.Monsters {
    abstract class Monster : Entity
    {
        private int _maxLife = 1000;
        public int Life = 1000;

        public Monster(){}

        public Monster(int life)
        {
            Life = life;
        }

        /// <summary>
        /// returns the life left
        /// </summary>
        public float HealthPercent
        {
            get { return (float)Life/(float)_maxLife * 100; }
        }

        /// <summary>
        /// call when it is the monsters turn. when called in your monster,
        /// inflict the damage on the enemy ship and return the type of damage
        /// inflicted
        /// </summary>
        public abstract int makeTurnDamage();

        /// <summary>
        /// This gets called when the ship fires upon the dragon
        /// </summary>
        public abstract void getTurnDamage(int type, bool hits);
    }
}
