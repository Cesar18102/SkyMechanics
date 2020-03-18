using System.Linq;
using System.Collections.Generic;

using SFML.System;
using SFML.Graphics;

using SkyMechanics.Geometry;

namespace SkyMechanics
{
    public class SkyObjectDrawable : SkyObject, Drawable
    {
        public Color BodyColor { get; private set; }
        public List<Point> Path { get; private set; } = new List<Point>();

        public SkyObjectDrawable(double mass, double radius, Color bodyColor,
                                 Point initPosition, Vector initSpeed = null, 
                                 Vector initAcceleration = null) : 
            base(mass, radius, initPosition, initSpeed, initAcceleration) => BodyColor = bodyColor;

        public override void Update(double timeElapsed, Vector power)
        {
            Path.Add(new Point(Position));
            base.Update(timeElapsed, power);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Shape shape = new CircleShape((float)Radius)
            {
                FillColor = BodyColor,
                Position = new Vector2f((float)Position.X, (float)Position.Y)
            };

            shape.Draw(target, states);
            target.Draw(Path.Select(pathPoint => new Vertex(new Vector2f((float)pathPoint.X, (float)pathPoint.Y), BodyColor)).ToArray(), PrimitiveType.LineStrip, states);

            /*Point accelerationEndPoint = Position + Acceleration;
            Vertex[] accelerationVectorVertices = new Vertex[] {
                new Vertex(new Vector2f((float)Position.X, (float)Position.Y), BodyColor),
                new Vertex(new Vector2f((float)accelerationEndPoint.X, (float)accelerationEndPoint.Y), BodyColor)
            };
            target.Draw(accelerationVectorVertices, PrimitiveType.LineStrip, states);*/
        }
    }
}
