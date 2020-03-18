using System;

namespace SkyMechanics.Geometry
{
    public static class GeometryMath
    {
        public static double LengthTo(this Point fromPoint, Point toPoint) =>
            new Vector(fromPoint, toPoint).Length;

        public static double LengthToByX(this Point fromPoint, Point toPoint) =>
            toPoint.X - fromPoint.X;

        public static double LengthToByY(this Point fromPoint, Point toPoint) =>
            toPoint.Y - fromPoint.Y;

        public static Vector GetUnityVector(this Vector vector) =>
            vector / vector.Length;

        public static Vector Rotate(this Vector vector, double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            return new Vector(
                vector.DX * cos - vector.DY * sin, 
                vector.DY * cos + vector.DX * sin
            );
        }
    }
}
