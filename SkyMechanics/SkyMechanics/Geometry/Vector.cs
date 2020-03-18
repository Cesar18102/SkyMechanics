using System;

namespace SkyMechanics.Geometry
{
    public class Vector
    {
        public double DX { get; protected set; }
        public double DY { get; protected set; }

        public double Length => Math.Sqrt(DX * DX + DY * DY);

        public static Vector operator *(Vector vector, double a) =>
            new Vector(vector.DX * a, vector.DY * a);

        public static Vector operator /(Vector vector, double a) =>
            vector * (1 / a);

        public static Vector operator +(Vector leftVector, Vector rightVector) =>
            new Vector(leftVector.DX + rightVector.DX, leftVector.DY + rightVector.DY);

        public static Vector operator -(Vector vector) =>
            new Vector(-vector.DX, -vector.DY);

        public static Vector operator -(Vector leftVector, Vector rightVector) =>
            leftVector + -rightVector;

        public static explicit operator Point(Vector vector) =>
            new Point(vector.DX, vector.DY);

        public Vector(Vector vector)
        {
            DX = vector.DX;
            DY = vector.DY;
        }

        public Vector(Point start, Point end)
        {
            DX = end.X - start.X;
            DY = end.Y - start.Y;
        }

        public Vector(double dx, double dy)
        {
            DX = dx;
            DY = dy;
        }
    }
}
