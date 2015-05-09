using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    internal class DragonClaw : Projectile
    {
        /// <summary>
        ///     Difference vector for scratching over the room
        /// </summary>
        private readonly Vector2f _scratchDirection;

        /// <summary>
        ///     Begin top left, scratch to bottom right constructor.
        /// </summary>
        /// <param name="target"></param>
        public DragonClaw(ITarget target) : base(target)
        {
            //init diff vector
            _scratchDirection = new Vector2f(5, 5);
            //load the claw sprite
            Sprite = new Sprite(Globals.ClawTexture)
            {
                Position = target.Center - new Vector2f(128, 128)
            };
        }

        /// <summary>
        ///     Finished?
        /// </summary>
        public override bool ShouldKill { get; set; }

        /// <summary>
        ///     Move claw, kill when top left is over center
        /// </summary>
        public override void Update()
        {
            //move over the room while attacking
            Position += _scratchDirection;
            if (Position.X - 20 > Target.Center.X || Position.Y - 20 > Target.Center.Y)
                ShouldKill = true;
        }

        /// <summary>
        ///     Nothing needs to be done while over target
        /// </summary>
        public override void WhileOverTarget()
        {
        }

        /// <summary>
        ///     The claw has no special impact action
        /// </summary>
        public override void OnImpact()
        {
        }

        /// <summary>
        ///     Always draw the claw
        /// </summary>
        public override void Draw()
        {
            Controller.Window.Draw(Sprite);
        }
    }
}