using SpaceBackend.Models;

namespace SpaceBackend.Repositories
{
    public interface IIDRepository
    {
        Task<List<Player>> GetPlayers(string sessionid);
    }
}
