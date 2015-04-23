using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    /// <summary>
    /// Projectiles that fly along a straight path, like the cannonball, from room to monster, with a static sprite
    /// </summary>
    abstract class KineticProjectile : Projectile
    {
        /// <summary>
        /// The direction this projectile travels in
        /// </summary>
        protected Vector2f Direction;

        /// <summary>
        /// The room the projectile started in
        /// </summary>
        private ITarget StartRoom;

        /// <summary>
        /// set the target monster, init impacthappened & shouldkill => false, calculate direction,
        /// add spritem set startposition
        /// </summary>
        /// <param name="target">Projectile target</param>
        /// <param name="startRoom">Projectile start</param>
        /// <param name="projectileTexture">The texture the Projectile should use</param>
        public KineticProjectile(ITarget target, ITarget startRoom, Texture projectileTexture) : base(target)
        {
            //calculate direction for straight space traversal
            Direction = (target.Center - startRoom.Center) / 70;
            
            //save room
            StartRoom = startRoom;

            //init sprite
            Sprite = new Sprite(projectileTexture);

            //set sprite position to start room
            Position = startRoom.Center;

            //definite init that the projectile should not be immediately killed...
            ShouldKill = false;
        }
    }
}
