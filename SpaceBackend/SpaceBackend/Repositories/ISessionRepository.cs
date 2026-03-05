using SpaceBackend.Models;

namespace SpaceBackend.Repositories
{
    public interface ISessionRepository
    {
        Task <Player> CreatePeople(string Id);
        Task <Player> GetPerson(string Id);
        Task <Session> CreateSession(string Id);
        Task <Session> FindSession(string Id);
        Task UpdPerson(Player person);
    }
}
