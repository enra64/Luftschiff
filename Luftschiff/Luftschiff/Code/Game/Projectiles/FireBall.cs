using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Global.Utils;
using SFML.Graphics;

namespace Luftschiff.Code.Game.Projectiles
{
    internal class FireBall : KineticProjectile
    {
        private readonly Room _attackedRoom;

        public FireBall(ITarget target, ITarget startRoom, Texture projectileTexture)
            : base(target, startRoom, projectileTexture)
        {
            _attackedRoom = (Room) target;
            //Wall of Sprite :/
            /*
            ImpactAnimationSprite = new AnimatedSprite(Time.FromSeconds(0.1f), false, false, Position);
            ImpactAnimation = new Animation(Globals.Cannon_Explosion);
            ImpactAnimation.AddFrame(new IntRect(0, 0, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(97, 0, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(194, 0, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(291, 0, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(388, 0, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(0, 96, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(97, 96, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(194, 96, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(291, 96, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(388, 96, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(0, 192, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(97, 192, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(194, 192, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(291, 192, 96, 97));
            ImpactAnimation.AddFrame(new IntRect(388, 192, 96, 97));*/
        }

        public override void Update()
        {
            //move while impact has not happened
            if (!ImpactHappened)
                Position += Direction;
        }

        public override void WhileOverTarget()
        {
            //SpritePlay();
            ShouldKill = true;
        }

        public override void OnImpact()
        {
            //randomise room ignition chance to make game more playable
            if (RandomHelper.RandomTrue(33))
            {
                Notifications.Instance.AddNotification(_attackedRoom.Position, "IGNITED");
                _attackedRoom.SetOnFire(3);
            }
            else
                Notifications.Instance.AddNotification(_attackedRoom.Position, "NO IGNITION");
        }

        public override void Draw()
        {
            //only draw when the impact has happened
            if (ImpactHappened)
                Controller.Window.Draw(ImpactAnimationSprite);
            else
                Controller.Window.Draw(Sprite);
        }
    }
}