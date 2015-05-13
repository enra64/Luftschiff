namespace Luftschiff.Graphics.Lib.Particles
{
    internal abstract class SpriteParticle : AParticles
    {
        //Similar to shape particle just with sprites
        public abstract AnimatedSprite Sprite { get; set; }
    }
}