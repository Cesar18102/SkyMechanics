using SFML.Graphics;

namespace SkyMechanics
{
    public class UniverseDrawable : Universe<SkyObjectDrawable>, Drawable
    {
        public UniverseDrawable(GravityPowerDelegate gravityPowerFormula) : 
            base(gravityPowerFormula) { }

        public void Draw(RenderTarget target, RenderStates states) =>
            SkyObjects.ForEach(skyObject => skyObject.Draw(target, states));
    }
}
