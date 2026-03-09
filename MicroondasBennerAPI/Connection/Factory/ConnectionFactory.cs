using System.Data;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Utils;
using Microsoft.Data.SqlClient;

namespace MicroondasBennerAPI.Connection.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            var connectionStringDescriptografada = CryptoUtils.Decrypt(
                Environment.GetEnvironmentVariable("CONNECTION_STRING_PADRAO") ?? string.Empty
            );

            return new SqlConnection(connectionStringDescriptografada);
        }
    }
}
