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

        public void DeleteSession(string SessionId)
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

        public Task<Session> GetSessionByID(string SessionId)
        {
            return _sessionRepository.GetSessionByID(SessionId);
        }

        public void UpdateSession(Session session)
        {
            _sessionRepository.UpdateSession(session);
        }

        //public bool SessionIsAvailableForSpin(string sessionId)
        //{
        //    if (!SessionIsAvailableForPlacingBets(sessionId) { return false; }
        //    if (!SpinHasRelatedBets(sessionId) { return false; }
        //    return true;

        //}

        public bool SessionIsAvailableForPlacingBets(string sessionId)
        {
            var session = _sessionRepository.GetSessionByID(sessionId);
            if (session is null) { return false; }
            if (session.Result is null) { return false; }
            if (session.Result.hasSpun) { return false; }
            return true;

        }

        public void AddBet(Bet bet)
        {
            _sessionRepository.AddBet(bet);
        }
        public Task<IEnumerable<Bet>> GetAllBetsForSession(string sessionId)
        {
            return _sessionRepository.GetAllBetsForSession(sessionId);
        }

        public Task<Bet> GetBetById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
