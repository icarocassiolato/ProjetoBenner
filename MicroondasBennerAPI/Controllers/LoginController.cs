using MicroondasBennerAPI.Service.Contracts;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroondasBennerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;

        [HttpPost()]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //Poderia ser feita uma consulta no banco de dados para validar o usuário,
            //mas para fins de exemplo, vamos usar um usuário fixo
            if (request.Email != "teste@benner.com" || request.Password != "123")
                return Unauthorized();

            var token = _loginService.GerarToken(request.Email);

            return Ok(new
            {
                token
            });
        }
    }
}