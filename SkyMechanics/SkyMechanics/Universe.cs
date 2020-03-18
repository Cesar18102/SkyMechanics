using System.Collections.Generic;

using SkyMechanics.Geometry;

namespace SkyMechanics
{
    public class Universe<T> where T : SkyObject
    {
        public delegate (Vector acceleration, Vector accelerationOther) GravityPowerDelegate(T skyObject, T skyObjectOther);
        public GravityPowerDelegate GravityPowerFormula { get; private set; }

        public List<T> SkyObjects { get; private set; } = new List<T>();

        public Universe(GravityPowerDelegate gravityPowerFormula) =>
            GravityPowerFormula = gravityPowerFormula;

        public void Update(double timeElapsed)
        {
            Dictionary<T, Vector> updateAcceleration = new Dictionary<T, Vector>();

            for (int i = 0; i < SkyObjects.Count; i++)
                updateAcceleration.Add(SkyObjects[i], new Vector(0, 0));

            for(int i = 0; i < SkyObjects.Count; i++)
            {
                Vector updatePowerVector = new Vector(0, 0);
                for (int j = i + 1; j < SkyObjects.Count; j++)
                {
                    (Vector acceleration, Vector accelerationOther) acceleration = GravityPowerFormula(SkyObjects[i], SkyObjects[j]);
                    updateAcceleration[SkyObjects[i]] += acceleration.acceleration;
                    updateAcceleration[SkyObjects[j]] += acceleration.accelerationOther;
                }
            }

            SkyObjects.ForEach(skyObject => skyObject.Update(timeElapsed, updateAcceleration[skyObject]));
        }
    }
}
