using DefaultNamespace;
using UnityEngine;

namespace Routs
{
    public class Normal : BasicRout
    {
        public Normal(string name, int distance) : base(name, distance)
        {
        }

        public override void CalculateTime(SpaceShip ship)
        {
            Debug.Log("Calculate time for " + ship + " " + Distance/ship.Speed);
        }
    }
}