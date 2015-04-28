using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Projectiles
{
    internal class CannonBall : KineticProjectile
    {
        public CannonBall(ITarget startRoom, ITarget target) : base(target, startRoom, Globals.CannonBallTexture)
        {
            //Wall of Sprite :/
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
            ImpactAnimation.AddFrame(new IntRect(388, 192, 96, 97));
        }

        public override void Update()
        {
            //move while impact has not happened
            if (!ImpactHappened)
                Position += Direction;
            //stop moving, but play the sprite
            else
                SpritePlay();
        }

        public override void WhileOverTarget()
        {
        }

        public override void Draw()
        {
            //only draw when not interacting and if no damage has been made yet
            if (!ImpactHappened)
                Controller.Window.Draw(Sprite);
            //draw the explosion sprite on impact
            if (ImpactHappened)
                Controller.Window.Draw(ImpactAnimationSprite);
        }
        
    }
}