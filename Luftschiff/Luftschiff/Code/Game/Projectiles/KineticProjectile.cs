﻿using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    /// <summary>
    ///     Projectiles that fly along a straight path, like the cannonball, from room to monster, with a static sprite
    ///     and an impact fx animated sprite
    /// </summary>
    internal abstract class KineticProjectile : Projectile
    {
        /// <summary>
        ///     The direction this projectile travels in
        /// </summary>
        protected Vector2f Direction;

        /// <summary>
        ///     Animation to play on impact
        /// </summary>
        protected Animation ImpactAnimation;

        /// <summary>
        ///     The animated sprite to be played once on impact
        /// </summary>
        protected AnimatedSprite ImpactAnimationSprite;

        /// <summary>
        ///     set the target monster, init impacthappened & shouldkill => false, calculate direction,
        ///     add spritem set startposition
        /// </summary>
        /// <param name="target">Projectile target</param>
        /// <param name="startRoom">Projectile start</param>
        /// <param name="projectileTexture">The texture the Projectile should use</param>
        public KineticProjectile(ITarget target, ITarget startRoom, Texture projectileTexture) : base(target)
        {
            //calculate direction for straight space traversal
            Direction = (target.Center - startRoom.Center)/70;

            //init sprite
            Sprite = new Sprite(projectileTexture);

            //set sprite position to start room
            Position = startRoom.Center;

            //definite init that the projectile should not be immediately killed...
            ShouldKill = false;
        }

        public override bool ShouldKill { get; set; }

        public override void OnImpact()
        {
            new Sound(Globals.BoomSound).Play();
        }

        /// <summary>
        ///     Plays Sprite once on Impact
        /// </summary>
        protected virtual void SpritePlay()
        {
            //update position to cannonball impact position
            ImpactAnimationSprite.Position = Position;

            //update the frame time
            ImpactAnimationSprite.Update(Globals.FRAME_TIME);

            //do magic
            if (!ShouldKill)
            {
                if (ImpactAnimationSprite.TimesPlayed*2 < ImpactAnimation.GetSize())
                    ImpactAnimationSprite.Play(ImpactAnimation);
                else
                //signal the collider that this bullet should be killed
                    ShouldKill = true;
            }
        }
    }
}