using SpaceBackend.Models;

namespace SpaceBackend.Services
{
    public interface IIDService
    {
        Task<GetSessionResponse> GetSessionAsync(string id);
    }
}
