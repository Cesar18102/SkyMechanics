using System;
using System.Linq;

using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace SkyMechanics
{
    public class Simulation
    {
        public double ScaleUnit { get; private set; }
        public double CurrentScale { get; private set; } = 1;

        public double SpeedUnit { get; private set; }
        public double CurrentSpeed { get; private set; } = 1;

        public bool Paused { get; private set; }
        public Color BackColor { get; private set; }

        public string Name { get; private set; }
        public RenderWindow Window { get; private set; }

        public SkyObject Origin { get; private set; }
        public UniverseDrawable Universe { get; private set; }

        public Simulation(string name, double scaleUnit, double speedUnit, Color backColor, UniverseDrawable universe)
        {
            BackColor = backColor;
            ScaleUnit = scaleUnit;
            SpeedUnit = speedUnit;

            Name = name;
            Universe = universe;
            Window = new RenderWindow(VideoMode.FullscreenModes[0], Name);

            Window.SetFramerateLimit(60);
            Window.KeyReleased += Window_KeyReleased;
            Window.Closed += (sender, e) => Window.Close();
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            double dScale = e.Code == Keyboard.Key.Add ? 1 / ScaleUnit : (e.Code == Keyboard.Key.Subtract ? ScaleUnit : 1);
            CurrentScale *= dScale;

            View temp = Window.GetView();
            temp.Zoom((float)dScale);
            Window.SetView(temp);

            CurrentSpeed *= (e.Code == Keyboard.Key.Right ? (SpeedUnit) : (e.Code == Keyboard.Key.Left ? 1 / SpeedUnit : 1));

            if (e.Code == Keyboard.Key.Space)
                LookAt(Universe.SkyObjects.Append(Universe.SkyObjects.First()).SkipWhile(SO => SO != Origin).ElementAt(1));
        }

        public void Start()
        {
            DateTime currentTime = DateTime.Now;

            while(Window.IsOpen)
            {
                Window.DispatchEvents();

                double elapsed = (DateTime.Now - currentTime).TotalSeconds;
                currentTime = DateTime.Now;

                if (Paused)
                    continue;

                View temp = Window.GetView();
                temp.Center = new Vector2f((float)Origin.Position.X, (float)Origin.Position.Y);
                Window.SetView(temp);

                Window.Clear(BackColor);
                Universe.Update(CurrentSpeed / 60.0);//elapsed
                Window.Draw(Universe);
                Window.Display();
            }
        }

        public void LookAt(SkyObject skyObject) =>
            Origin = skyObject;

        public void Pause() =>
            Paused = true;

        public void Continue() =>
            Paused = false;
    }
}
