using SpaceBackend.Repositories;
using SpaceBackend.Models;

namespace SpaceBackend.Services
{
    public class IDService : IIDService
    {
        public readonly IIDRepository _iDRepository;

        public IDService( IIDRepository iDRepository ) { _iDRepository = iDRepository;}

        public async Task<GetSessionResponse> GetSessionAsync(string id)
        {
            var response = new GetSessionResponse();
            response.people = await _iDRepository.GetPlayers(id);
            response.PeopleCount = response.people.Count;
            response.SessionId = id;
            return response;
        }
    }
}
