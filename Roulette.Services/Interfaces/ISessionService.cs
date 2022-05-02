using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllSessions();
        Task<Session> GetSessionByID(Guid SessionId);
        Task<Session> GetActiveSession();
        Task<IEnumerable<Session>> GetDoneSessions();
        void AddSession(Session session);
        void UpdateSession(Session session);
        void DeleteSession(Guid SessionId);
        public bool SessionIsAvailableForSpin(string sessionId);
    }
}
