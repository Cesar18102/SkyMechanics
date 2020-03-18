using SkyMechanics.Geometry;

namespace SkyMechanics
{
    public class SkyObject
    {
        public double Mass { get; protected set; }
        public double Radius { get; private set; }
        public Point Position { get; protected set; }
        public Vector Speed { get; protected set; } = new Vector(0, 0);
        public Vector Acceleration { get; protected set; } = new Vector(0, 0);

        public SkyObject(double mass, double radius, Point initPosition, Vector initSpeed = null, Vector initAcceleration = null)
        {
            Mass = mass;
            Radius = radius;
            Position = initPosition;

            if (initSpeed != null)
                Speed = initSpeed;

            if (initAcceleration != null)
                Acceleration = initAcceleration;
        }

        public virtual void Update(double timeElapsed, Vector acceleration)
        {
            Position.MoveBy(Speed * timeElapsed + Acceleration * (timeElapsed * timeElapsed / 2));
            Acceleration = acceleration;
            Speed += Acceleration * timeElapsed;
        }
    }
}
