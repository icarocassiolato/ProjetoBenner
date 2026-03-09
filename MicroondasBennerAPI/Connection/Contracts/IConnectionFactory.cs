using System.Data;

namespace MicroondasBennerAPI.Connection.Contracts
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
    }
}
