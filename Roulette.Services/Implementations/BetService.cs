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

        public void DeleteBet(Guid BetId)
        {
            _betRepository.DeleteBet(BetId);
        }

        public Task<IEnumerable<Bet>> GetAllBetsBySessionId(Guid SessionId)
        {
            return _betRepository.GetAllBetsBySessionId(SessionId);
        }

        public Task<Bet> GetBetById(Guid BetId)
        {
            return _betRepository.GetBetById(BetId);
        }

        public Task<Bet> GetBetBySessionId(Guid SessionId)
        {
            return _betRepository.GetBetBySessionId(SessionId);
        }

        public void UpdateBet(Bet bet)
        {
            _betRepository.UpdateBet(bet);
        }
    }
}
