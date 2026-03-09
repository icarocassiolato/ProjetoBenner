using Dapper;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Repository.Repositories
{
    public class ErroRepository(IConnectionFactory connectionFactory) : IErroRepository
    {
        private readonly IConnectionFactory _connectionFactory = connectionFactory;

        public async Task<int> InsertAsync(string descricao)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"
        INSERT INTO Erro
        (
            Descricao
        )
        VALUES
        (
            @Descricao
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, new { descricao });
        }
    }
}
