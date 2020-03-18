using SFML.Graphics;
using SkyMechanics.Geometry;
using System;

namespace SkyMechanics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UniverseDrawable universe = new UniverseDrawable((skyObject, skyObjectOther) =>
            {
                const double G = 0.67E-11;
                Vector unityPowerVector = new Vector(skyObject.Position, skyObjectOther.Position).GetUnityVector();
                double distance = skyObject.Position.LengthTo(skyObjectOther.Position);
                return (unityPowerVector * (G * skyObjectOther.Mass / (distance * distance)), 
                        -unityPowerVector * (G * skyObject.Mass / (distance * distance)));
            });

            //SkyObjectDrawable Solar1 = new SkyObjectDrawable(2E17, 5, Color.Green, new Point(500, 500), new Vector(-75, 0));
            //SkyObjectDrawable Solar2 = new SkyObjectDrawable(3E17, 5, Color.Red, new Point(567, 600), new Vector(100, 0));

            SkyObjectDrawable Earth = new SkyObjectDrawable(3E15, 5, Color.Blue, new Point(500, 0), new Vector(0, 0));
            SkyObjectDrawable Moon = new SkyObjectDrawable(3E3, 5, Color.Yellow, new Point(500, 200), new Vector(9.4, 0));

            //universe.SkyObjects.Add(Solar1);
            //universe.SkyObjects.Add(Solar2);

            universe.SkyObjects.Add(Earth);
            universe.SkyObjects.Add(Moon);

            Simulation simulation = new Simulation("test", 1.5f, 2f, Color.Black, universe);
            simulation.LookAt(Earth);
            simulation.Start();
        }
    }
}
