namespace SkyMechanics.Geometry
{
    public class Point
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }

        public static explicit operator Vector(Point point) =>
            new Vector(point.X, point.Y);

        public static Point operator +(Point point, Vector vector) =>
            new Point(point.X + vector.DX, point.Y + vector.DY);

        public static Point operator -(Point point, Vector vector) =>
            point + -vector;

        public void MoveBy(Vector vector)
        {
            X += vector.DX;
            Y += vector.DY;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
