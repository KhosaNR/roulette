using Domain.Entities;
using Roulette.DataAccess.Dapper.Interfaces;
using Roulette.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Services.Implementations
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;
         
        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }
        public void AddBet(Bet bet)
        {
            _betRepository.AddBet(bet);
        }

        public void DeleteBet(string BetId)
        {
            _betRepository.DeleteBet(BetId);
        }

        public Task<IEnumerable<Bet>> GetActiveBetsBySessionId(string sessionId)
        {
            return _betRepository.GetActiveBetsBySessionId(sessionId);
        }

        public Task<Bet> GetBetById(string BetId)
        {
            return _betRepository.GetBetById(BetId);
        }

        public Task<Bet> GetBetBySessionId(string SessionId)
        {
            return _betRepository.GetBetBySessionId(SessionId);
        }

        public void UpdateBet(Bet bet)
        {
            _betRepository.UpdateBet(bet);
        }
    }
}
