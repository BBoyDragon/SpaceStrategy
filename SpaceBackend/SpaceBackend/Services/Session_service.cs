using SpaceBackend.Models;
using SpaceBackend.Repositories;

namespace SpaceBackend.Services
{
    public class Session_service : ISessionServicecs
    {
        private ISessionRepository _sessionRepository;
        public Session_service(ISessionRepository a)
        {
            _sessionRepository = a;
        }
        public async Task JoinToRoom(JoinSessionRequest Request)
        {
            Player player = await _sessionRepository.GetPerson(Request.player_id);
            if (player == null) player = await _sessionRepository.CreatePeople(Request.player_id);
            Session session = await _sessionRepository.FindSession(Request.session_id);
            if (session == null) session = await _sessionRepository.CreateSession(Request.session_id);
            player.Session = session;
            await _sessionRepository.UpdPerson(player);
        }
        public async Task LeaveToRoom(LeaveSessionRequestcs Request)
        {
            Player player = await _sessionRepository.GetPerson(Request.PlayerId);
            player.Session = null;
            await _sessionRepository.UpdPerson(player);
        }
    }
}
