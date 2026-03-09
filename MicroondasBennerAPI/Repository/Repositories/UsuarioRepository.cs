using Dapper;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Repository.Repositories
{
    public class UsuarioRepository(IConnectionFactory connectionFactory) : IUsuarioRepository
    {
        private readonly IConnectionFactory _connectionFactory = connectionFactory;

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var connection = _connectionFactory.Connection();

            var sql = "SELECT * FROM Usuario";

            return await connection.QueryAsync<Usuario>(sql);
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();

            var sql = "SELECT * FROM Usuario WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { id });
        }

        public async Task<int> InsertAsync(Usuario usuario)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"
        INSERT INTO Usuario
        (
            Nome,
            Senha
        )
        VALUES
        (
            @Nome,
            @Senha
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, usuario);
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"
        UPDATE Usuario
        SET
            Nome = @Nome,
            Senha = @Senha
        WHERE Id = @Id";

            var rows = await connection.ExecuteAsync(sql, usuario);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.Connection();

            var sql = "DELETE FROM Usuario WHERE Id = @Id";

            var rows = await connection.ExecuteAsync(sql, new { id });

            return rows > 0;
        }
    }
}
