using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerAPI.Utils;
using MicroondasBennerCommon.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroondasBennerAPI.Service.Services
{
    public class LoginService(ILoginRepository loginRepository) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;

        private static string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome)
                ]),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> UsuarioExiste(Usuario usuario)
        {
            usuario.Senha = HashUtils.GerarSha1(usuario.Senha);
            usuario.Id = await _loginRepository.UsuarioExiste(usuario);

            if (usuario.Id == 0)
                return string.Empty;

            return GerarToken(usuario);
        }
    }
}
