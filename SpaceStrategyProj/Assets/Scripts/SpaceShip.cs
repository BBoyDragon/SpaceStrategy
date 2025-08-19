namespace DefaultNamespace
{
    public class SpaceShip
    {
        public string Name { get; }
        public int Speed { get; }
        public int Agility { get; }
        public int Armor { get; }
        public int Power { get; }

        public SpaceShip(string name, int speed, int agility, int armor, int power)
        {
            Name = name;
            Speed = speed;
            Agility = agility;
            Armor = armor;
            Power = power;
        }
    }
}