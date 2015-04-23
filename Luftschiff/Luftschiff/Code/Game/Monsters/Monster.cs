using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Projectiles;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters {
    abstract class Monster : Entity, ITarget
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
        /// <param name="areaReference"></param>
        public abstract int AttackShip(Area areaReference);

        /// <summary>
        /// Implement the receive damage interface. The entity should get damage here.
        /// </summary>
        /// <param name="damageAmount"></param>
        public abstract void ReceiveDamage(int damageAmount);

        public abstract bool HasBeenHit(Vector2f projectilePosition);
    }
}
