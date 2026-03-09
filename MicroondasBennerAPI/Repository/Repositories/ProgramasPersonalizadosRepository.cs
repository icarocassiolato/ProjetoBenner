using Dapper;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerCommom.Models;

namespace MicroondasBennerAPI.Repository.Repositories
{
    public class ProgramasPersonalizadosRepository(IConnectionFactory connectionFactory) : IProgramasPersonalizadosRepository
    {
        private readonly IConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<ProgramaAquecimento>> GetAllAsync()
        {
            using var connection = _connectionFactory.Connection();

            var sql = "SELECT * FROM ProgramaPersonalizado";

            return await connection.QueryAsync<ProgramaAquecimento>(sql);
        }

        public async Task<ProgramaAquecimento?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();

            var sql = "SELECT * FROM ProgramaPersonalizado WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<ProgramaAquecimento>(sql, new { id });
        }

        public async Task<int> InsertAsync(ProgramaAquecimento programa)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"
        INSERT INTO ProgramaPersonalizado
        (
            Tempo,
            Potencia,
            Nome,
            SimboloProgresso,
            Instrucoes
        )
        VALUES
        (
            @Tempo,
            @Potencia,
            @Nome,
            @SimboloProgresso,
            @Instrucoes
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, programa);
        }

        public async Task<bool> UpdateAsync(ProgramaAquecimento programa)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"
        UPDATE ProgramaPersonalizado
        SET
            Tempo = @Tempo,
            Potencia = @Potencia,
            Nome = @Nome,
            SimboloProgresso = @SimboloProgresso,
            Instrucoes = @Instrucoes
        WHERE Id = @Id";

            var rows = await connection.ExecuteAsync(sql, programa);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.Connection();

            var sql = "DELETE FROM ProgramaPersonalizado WHERE Id = @Id";

            var rows = await connection.ExecuteAsync(sql, new { id });

            return rows > 0;
        }
    }
}