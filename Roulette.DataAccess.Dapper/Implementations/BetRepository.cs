using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Roulette.DataAccess.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.DataAccess.Dapper.Implementations
{
    public class BetRepository: IBetRepository
    {
        protected readonly IConfiguration _config;
        public BetRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SQLiteConnection(_config.GetConnectionString("SqliteDB"));
            }
        }

        public void AddBet(Bet bet)
        {

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var query = "INSERT INTO BET " +
                        "(ID, BETTYPEID, NUMBERS, BETAMOUNT, SESSIONID)" +
                        $"VALUES ('{bet.Id.ToString("N")}',{bet.betTypeId},'{bet.numbers}','{bet.betAmount}','{bet.sessionId.ToString("N")}')";
                    dbConnection.Execute(query);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBet(Guid BetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bet>> GetAllBetsBySessionId(Guid SessionId)
        {
            throw new NotImplementedException();
        }

        public Task<Bet> GetBetById(Guid BetId)
        {
            throw new NotImplementedException();
        }

        public Task<Bet> GetBetBySessionId(Guid SessionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBet(Bet bet)
        {
            throw new NotImplementedException();
        }
    }
}
