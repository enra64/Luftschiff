using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    class DragonClaw : Projectile
    {
        private readonly Vector2f _scratchDirection;

        public DragonClaw(ITarget target): base(target)
        {
            _scratchDirection = new Vector2f(5, 5);
            Sprite = new Sprite(Globals.ClawTexture);
            Sprite.Position = target.Center - new Vector2f(128, 128);
        }

        public override bool ShouldKill { get; set; }

        public override void Update()
        {
            //move over the room while attacking
            Position += _scratchDirection;
            if (Position.X - 20 > Target.Center.X || Position.Y - 20 > Target.Center.Y)
                ShouldKill = true;
        }

        public override void WhileOverTarget()
        {
        }

        public override void OnImpact()
        {
        }

        public override void Draw() {
            //only draw when the impact has happened
            Controller.Window.Draw(Sprite);
        }
    }
}
