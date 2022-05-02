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
                        $"VALUES ('{bet.Id}',{bet.betTypeId},'{string.Join(",",bet.numbers)}','{bet.betAmount}','{bet.sessionId}')";
                    dbConnection.Execute(query);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBet(string BetId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bet>> GetActiveBetsBySessionId(string sessionId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<Bet>($"SELECT * FROM BET INNER JOIN SESSION ON BET.SESSIONID = SESSION.ID WHERE BET.SESSIONID = '{sessionId}' AND SESSION.HASSPUN<>0");
            }
        }

        public Task<Bet> GetBetById(string BetId)
        {
            throw new NotImplementedException();
        }

        public Task<Bet> GetBetBySessionId(string SessionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBet(Bet bet)
        {
            throw new NotImplementedException();
        }
    }
}
