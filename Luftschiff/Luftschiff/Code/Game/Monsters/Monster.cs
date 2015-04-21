using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Game.Monsters {
    abstract class Monster : Entity
    {
        private float _maxLife;
        internal float Life;

        /// <summary>
        /// Init Life and maxLife
        /// </summary>
        /// <param name="life"></param>
        protected Monster(int life)
        {
            Life = life;
            _maxLife = life;
        }

        /// <summary>
        /// returns the life left
        /// </summary>
        public float HealthPercent
        {
            get { return (Life/_maxLife) * 100; }
        }

        /// <summary>
        /// call when it is the monsters turn. when called in your monster,
        /// inflict the damage on the enemy ship and return the type of damage
        /// inflicted
        /// </summary>
        public abstract int AttackShip();

        /// <summary>
        /// This gets called when the ship fires upon the dragon
        /// </summary>
        public abstract void ReceiveDamageByShip(int type, bool hits);
    }
}
