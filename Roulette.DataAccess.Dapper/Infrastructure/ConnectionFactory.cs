using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.DataAccess.Dapper.Infrastructure
{
    internal class ConnectionFactory: IConnectionFactory
    { /*
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DTAppCon"].ConnectionString;
        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }

        protected readonly IConfiguration _config;
        public BetRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                var configuration = new Configuration();
                configuration.AddJsonFile("config.json");
                var emailAddress = configuration.Get("emailAddress");
                IConfiguration config = new ConfigurationBuilder()
                return new SQLiteConnection(config.GetConnectionString("SqliteDB"));
            }
        }*/
    }
}
