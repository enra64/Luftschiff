using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Projectiles;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters
{
    internal abstract class Monster : Entity, ITarget
    {
        private readonly float _maxLife;
        internal float Life;

        /// <summary>
        ///     Init Life and maxLife
        /// </summary>
        /// <param name="life"></param>
        protected Monster(int life)
        {
            Life = life;
            _maxLife = life;
        }

        /// <summary>
        ///     returns the life left
        /// </summary>
        public float HealthPercent
        {
            get { return (Life/_maxLife)*100; }
        }

        /// <summary>
        ///     Implement the receive damage interface. The entity should get damage here.
        /// </summary>
        /// <param name="damageAmount"></param>
        public abstract void ReceiveDamage(int damageAmount);

        public abstract bool HasBeenHit(Vector2f projectilePosition);

        /// <summary>
        ///     call when it is the monsters turn. when called in your monster,
        ///     inflict the damage on the enemy ship and return the type of damage
        ///     inflicted
        /// </summary>
        /// <param name="areaReference"></param>
        public abstract void AttackShip(Area areaReference);
    }
}