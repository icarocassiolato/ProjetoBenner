using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasBennerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var token = await _loginService.UsuarioExiste(usuario);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(new
            {
                token
            });
        }
    }
}