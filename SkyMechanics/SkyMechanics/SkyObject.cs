using SkyMechanics.Geometry;

namespace SkyMechanics
{
    public class SkyObject
    {
        public string Name { get; protected set; }
        public double Mass { get; protected set; }
        public double Radius { get; protected set; }
        public Point Position { get; protected set; }
        public Vector Speed { get; set; } = new Vector(0, 0);
        public Vector Acceleration { get; protected set; } = new Vector(0, 0);

        public SkyObject(string name, double mass, double radius, Point initPosition) :
            this(name, mass, radius, initPosition, null, null) { }

        public SkyObject(string name, double mass, double radius, 
                         Point initPosition, Vector initSpeed) : 
            this(name, mass, radius, initPosition, initSpeed, null) { }

        public SkyObject(string name, double mass, double radius,
                         Point initPosition, SkyObject parentSun) :
            this(name, mass, radius, initPosition, null, parentSun) { }

        public SkyObject(string name, double mass, double radius,
                         Point initPosition, Vector initSpeed, SkyObject parentSun)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            Position = initPosition;

            if (initSpeed != null)
                Speed = initSpeed;

            if (parentSun != null)
                Speed += parentSun.GetSpeedForCircle(this);
        }

        public virtual void Update(double timeElapsed, Vector acceleration)
        {
            Acceleration = acceleration;
            Speed += Acceleration * timeElapsed;
            Position.MoveBy(Speed * timeElapsed);
            // + Acceleration * (timeElapsed * timeElapsed / 2));
        }
    }
}
