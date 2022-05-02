using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Services.Interfaces
{
    public interface IBetService
    {
        Task<Bet> GetBetById(string BetId);
        Task<Bet> GetBetBySessionId(string SessionId);
        Task<IEnumerable<Bet>> GetActiveBetsBySessionId(string SessionId);
        void AddBet(Bet bet);
        void UpdateBet(Bet bet);
        void DeleteBet(string BetId);
    }
}
