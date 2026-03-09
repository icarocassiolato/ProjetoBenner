using Dapper;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Repository.Repositories
{
    public class LoginRepository(IConnectionFactory connectionFactory) : ILoginRepository
    {
        private readonly IConnectionFactory _connectionFactory = connectionFactory;

        public async Task<int> UsuarioExiste(Usuario usuario)
        {
            using var connection = _connectionFactory.Connection();

            var sql = @"SELECT Id 
                FROM Usuario 
                WHERE Nome = @Nome
                AND Senha = @Senha";

            return (await connection.QueryAsync<Usuario>(sql, usuario)).FirstOrDefault()?.Id ?? 0;
        }
    }
}
