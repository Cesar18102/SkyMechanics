using SFML.Graphics;

using SkyMechanics.Geometry;

namespace SkyMechanics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UniverseDrawable universe = new UniverseDrawable((skyObject, skyObjectOther) =>
            {
                Vector unityPowerVector = new Vector(skyObject.Position, skyObjectOther.Position).GetUnityVector();
                double distance = skyObject.Position.LengthTo(skyObjectOther.Position);
                return (unityPowerVector * (SkyMath.G * skyObjectOther.Mass / (distance * distance)),
                        -unityPowerVector * (SkyMath.G * skyObject.Mass / (distance * distance)));
            });

            SkyObjectDrawable Solar = new SkyObjectDrawable("SUN1", 2E17, 5, new Point(300, -100), Color.Green);
            SkyObjectDrawable Earth = new SkyObjectDrawable("EARTH", 8E12, 5, new Point(300, 200), Solar, Color.Blue);
            SkyObjectDrawable Mars = new SkyObjectDrawable("MARS", 9E13, 5, new Point(300, 220), Solar, Color.Red);

            universe.SkyObjects.Add(Solar);
            universe.SkyObjects.Add(Earth);
            universe.SkyObjects.Add(Mars);

            Simulation simulation = new Simulation("test", 1.5f, 2f, Color.Black, universe);
            simulation.LookAt(Solar);
            simulation.Start();
        }
    }
}
