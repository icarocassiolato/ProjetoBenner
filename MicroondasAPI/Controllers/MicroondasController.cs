using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroondasApi.Service.Contracts;

namespace MicroondasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MicroondasController : ControllerBase
    {
        private readonly IMicroondasAppService _appService;

        public MicroondasController(IMicroondasAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] StartRequest req)
        {
            try
            {
                await _appService.StartAsync(req.TipoPrograma, req.Tempo, req.Potencia);
                return Ok(new { status = await _appService.GetStatusAsync() });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("add30")]
        public async Task<IActionResult> Add30()
        {
            await _appService.Add30Async();
            return Ok(new { status = await _appService.GetStatusAsync() });
        }

        [HttpPost("pauseOrCancel")]
        public async Task<IActionResult> PauseOrCancel()
        {
            await _appService.PauseOrCancelAsync();
            return Ok(new { status = await _appService.GetStatusAsync() });
        }

        [HttpGet("status")]
        public async Task<IActionResult> Status() => Ok(new { status = await _appService.GetStatusAsync() });

        public class StartRequest
        {
            public int TipoPrograma { get; set; }
            public int? Tempo { get; set; }
            public int? Potencia { get; set; }
        }
    }
}