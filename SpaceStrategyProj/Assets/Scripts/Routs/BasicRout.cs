using DefaultNamespace;
using UnityEngine;

namespace Routs
{
    public abstract class BasicRout
    {
        public string Name { get; }
        public int Distance { get; }

        public BasicRout(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }

        public abstract void CalculateTime(SpaceShip ship);
    }
}