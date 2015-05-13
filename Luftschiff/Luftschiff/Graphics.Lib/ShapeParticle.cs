using SFML.Graphics;

namespace Luftschiff.Graphics.Lib
{
    internal abstract class ShapeParticle : AParticles
    {
        //Well this is more ore less self explanatory
        public abstract Shape Shape { get; set; }
    }
}