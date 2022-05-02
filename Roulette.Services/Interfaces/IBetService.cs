﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Services.Interfaces
{
    public interface IBetService
    {
        Task<Bet> GetBetById(Guid BetId);
        Task<Bet> GetBetBySessionId(Guid SessionId);
        Task<IEnumerable<Bet>> GetAllBetsBySessionId(Guid SessionId);
        void AddBet(Bet bet);
        void UpdateBet(Bet bet);
        void DeleteBet(Guid BetId);
    }
}
