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
                    //dbConnection.Open();
                    var query = $"INSERT INTO SESSION (Id, hasSpun, winningNumber) " +
                        $"VALUES ('{session.Id}',{session.hasSpun},'{session.winningNumber}')";
                    dbConnection.Execute(query);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSession(Guid SessionId)
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
                    return await dbConnection.QueryAsync<Session>("SELECT * FROM SESSION");
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
                    return await dbConnection.QueryAsync<Session>("SELECT * FROM SESSION WHERE HASSPUN <> 0");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Session> GetSessionByID(Guid SessionId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    return await dbConnection.QueryFirstAsync<Session>($"SELECT * FROM SESSION WHERE ID = '{SessionId.ToString("N")}'");
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
                    var sqlCommand = $"UPDATE SESSION SET HasSpun = {session.hasSpun}, WinningNUmber = {session.winningNumber} " +
                        $"WHERE ID = '{session.Id}'";
                    dbConnection.Execute(sqlCommand);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
