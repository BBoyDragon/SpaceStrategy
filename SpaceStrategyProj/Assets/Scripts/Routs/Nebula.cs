using DefaultNamespace;
using UnityEngine;

namespace Routs
{
    public class Nebula : BasicRout
    {
        public Nebula(string name, int distance) : base(name, distance)
        {
        }

        public override void CalculateTime(SpaceShip ship)
        {
            Debug.Log("Calculate time for " + ship + " " + Distance/ship.Power);
        }
    }
}