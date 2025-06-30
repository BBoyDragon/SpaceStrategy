using DefaultNamespace;
using UnityEngine;

namespace Routs
{
    public class AsteroidField : BasicRout
    {
        public AsteroidField(string name, int distance) : base(name, distance)
        {
        }

        public override void CalculateTime(SpaceShip ship)
        {
            Debug.Log("Calculate time for " + ship + " " + Distance/ship.Armor);
        }
    }
}