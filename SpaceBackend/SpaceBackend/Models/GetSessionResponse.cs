namespace SpaceBackend.Models
{
    public class GetSessionResponse
    {
        public string SessionId { get; set; }
        public int PeopleCount;
        public List<Player> people;
        public List<Ship> ships;
    }
}
