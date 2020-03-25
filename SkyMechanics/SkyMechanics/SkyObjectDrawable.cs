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

        public SkyObjectDrawable(string name, double mass, double radius, 
                                 Point initPosition, Color bodyColor) :
            base(name, mass, radius, initPosition) => BodyColor = bodyColor;

        public SkyObjectDrawable(string name, double mass, double radius,
                                 Point initPosition, Vector initSpeed, Color bodyColor) :
            base(name, mass, radius, initPosition, initSpeed) => BodyColor = bodyColor;

        public SkyObjectDrawable(string name, double mass, double radius,
                                 Point initPosition, SkyObject parentSun, Color bodyColor) :
            base(name, mass, radius, initPosition, parentSun) => BodyColor = bodyColor;

        public SkyObjectDrawable(string name, double mass, double radius,
                                 Point initPosition, Vector initSpeed,
                                 SkyObject parentSun, Color bodyColor) :
            base(name, mass, radius, initPosition, initSpeed, parentSun) => BodyColor = bodyColor;

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
            target.Draw(
                Path.Select(pathPoint => 
                    new Vertex(
                        new Vector2f((float)pathPoint.X, (float)pathPoint.Y), 
                        BodyColor
                    )
                ).ToArray(), 
                PrimitiveType.LineStrip, states
            );
        }
    }
}
