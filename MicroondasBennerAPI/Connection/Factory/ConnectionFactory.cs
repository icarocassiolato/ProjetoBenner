using System.Data;
using MicroondasBennerAPI.Connection.Contracts;
using Microsoft.Data.SqlClient;

namespace MicroondasBennerAPI.Connection.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection() 
            => new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING_PADRAO"));
    }
}
