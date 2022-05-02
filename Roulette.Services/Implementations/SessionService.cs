using Domain.Entities;
using Roulette.DataAccess.Dapper.Implementations;
using Roulette.DataAccess.Dapper.Interfaces;
using Roulette.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Services.Implementations
{
    public class SessionService : ISessionService
    {
        protected readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public void AddSession(Session session)
        {
            _sessionRepository.AddSession(session);
        }

        public void DeleteSession(Guid SessionId)
        {
            _sessionRepository.DeleteSession(SessionId);
        }

        public Task<IEnumerable<Session>> GetAllSessions()
        {
            return _sessionRepository.GetAllSessions();
        }

        public Task<Session> GetActiveSession()
        {
            return _sessionRepository.GetActiveSession();
        }

        public Task<IEnumerable<Session>> GetDoneSessions()
        {
            return _sessionRepository.GetDoneSessions();
        }

        public Task<Session> GetSessionByID(Guid SessionId)
        {
            return _sessionRepository.GetSessionByID(SessionId);
        }

        public void UpdateSession(Session session)
        {
            _sessionRepository.UpdateSession(session);
        }

        public bool SessionIsAvailableForSpin(string sessionId)
        {
            var session = _sessionRepository.GetSessionByID(Guid.Parse(sessionId));
            if(session is null) { return false; }
            if(session.Result is null) { return false; }
            if (session.Result.hasSpun) { return false; }
            return true;

        }
    }
}
