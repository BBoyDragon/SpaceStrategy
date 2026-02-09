using SpaceBackend.Models;

namespace SpaceBackend.Services
{
    public interface ISessionServicecs
    {
        Task JoinToRoom(JoinSessionRequest Request);
        Task LeaveToRoom(LeaveSessionRequestcs Request);
    }
}
