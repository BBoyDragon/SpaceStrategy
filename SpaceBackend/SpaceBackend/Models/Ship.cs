namespace SpaceBackend.Models
{
    public class Ship
    {
        public int energy;
        public int energyregen;
        public int shield;
        public int shieldmax;
        public int shieldregen;
        public int capturer;
        public int speed;
        public int warprange;
        public int firepowermin;
        public int firepowermax;
        public int firerange;
        public int potentialdamage = 0;
        public string playername;
        public int x;
        public int y;  
        public int z;
        public Player player;
    }
}
