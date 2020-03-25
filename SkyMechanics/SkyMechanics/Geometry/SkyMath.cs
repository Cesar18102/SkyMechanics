using System;

namespace SkyMechanics.Geometry
{
    public static class SkyMath
    {
        public const double G = 6.67E-11;
        public const double c = 299792458;

        public static double GetGravityRadius(this SkyObject sun, SkyObject planet) =>
            2 * G * (sun.Mass + planet.Mass) / (c * c);

        public static double GetOrbitSpeedAbs(this SkyObject sun, SkyObject planet) =>
            Math.Sqrt(G * (sun.Mass + planet.Mass) / (planet.Position.LengthTo(sun.Position) - GetGravityRadius(sun, planet)));

        public static Vector GetSpeedForCircle(this SkyObject sun, SkyObject planet) =>
            new Vector(planet.Position, sun.Position).GetUnityVector().Rotate(Math.PI / 2) * GetOrbitSpeedAbs(sun, planet);
    }
}
