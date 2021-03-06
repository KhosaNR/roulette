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
    public class SessionRepository: ISessionRepository
    {
        protected readonly IConfiguration _config;
        public SessionRepository(IConfiguration configuration)
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

        public void AddSession(Session session)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var query = $"INSERT INTO SESSION (Id, hasSpun, winningNumber) " +
                        $"VALUES ('{session.Id}',{session.HasSpun},'{session.WinningNumber}')";
                    dbConnection.Execute(query);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSession(string SessionId)
        {

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Execute($"DELETE FROM SESSION WHERE ID = '{SessionId}'");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Session> GetActiveSession()
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    return await dbConnection.QueryFirstAsync<Session>($"SELECT * FROM SESSION WHERE HasSpun = 0");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Session>> GetAllSessions()
        {

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var lookup = new Dictionary<string, Session>();
                    return await dbConnection.QueryAsync<Session, Bet, Session>(@"
                                    SELECT s.*, b.*
                                    FROM Session s
                                    LEFT JOIN Bet b ON s.Id = b.SessionId                    
                                    ", (s, b) => {
                        Session session;
                        if (!lookup.TryGetValue(s.Id, out session))
                        {
                            lookup.Add(s.Id, session = s);
                        }
                        if (session.Bets == null)
                            session.Bets = new List<Bet>();
                            session.Bets.Add(b);
                        return session;
}
                 );
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Session>> GetDoneSessions()
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var lookup = new Dictionary<string, Session>();
                    return await dbConnection.QueryAsync<Session, Bet, Session>(@"
                                    SELECT s.*, b.*
                                    FROM Session s
                                    LEFT JOIN Bet b ON s.Id = b.SessionId                    
                                    WHERE hasSpun <> 0
                                    ", (s, b) => {
                        Session session;
                        if (!lookup.TryGetValue(s.Id, out session))
                        {
                            lookup.Add(s.Id, session = s);
                        }
                        if (session.Bets == null)
                            session.Bets = new List<Bet>();
                        session.Bets.Add(b);
                        return session;
                    }
                 );
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Session> GetSessionByID(string sessionId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var query = $@"
                                    SELECT s.*, b.*
                                    FROM Session s
                                    LEFT JOIN Bet b ON s.Id = b.SessionId                    
                                    WHERE s.ID = '{sessionId}'
                                    ";
                    var session = await dbConnection.QueryAsync<Session,Bet,Session>(query, (s, b) =>
                    {
                        if (s.Bets == null)
                            s.Bets = new List<Bet>();
                        s.Bets.Add(b);
                        return s;
                    }
                 );
                    return session.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateSession(Session session)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var sqlCommand = $"UPDATE SESSION SET HasSpun = {session.HasSpun}, WinningNUmber = {session.WinningNumber} " +
                        $"WHERE ID = '{session.Id}'";
                    dbConnection.Execute(sqlCommand);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                        $"VALUES ('{bet.Id}',{bet.BetTypeId},'{string.Join(",", bet.Numbers)}','{bet.BetAmount}','{bet.SessionId}')";
                    dbConnection.Execute(query);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Bet>> GetAllBetsForSession(string sessionId)
        {

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    return await dbConnection.QueryAsync<Bet>($"SELECT * FROM BET WHERE ID = '{sessionId}'");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Bet> GetBetById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
