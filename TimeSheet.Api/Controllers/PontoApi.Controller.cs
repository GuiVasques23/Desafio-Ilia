using Microsoft.AspNetCore.Mvc;
using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Services;
using Time.Sheet.Domain.Utils;

namespace Time.Sheet.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class PontoApiController : ControllerBase
    {
        private readonly IBatidasService _batidasService;
        private readonly IFolhasDePontoService _folhasDePontoService;

        public PontoApiController(IBatidasService batidasService, IFolhasDePontoService folhasDePontoService)
        {
            _batidasService = batidasService;
            _folhasDePontoService = folhasDePontoService;
        }

        [HttpPost("batidas")]
        public async Task<IActionResult> InsereBatida([FromBody] Momento momento)
        {
            ResponseResult responseResult = await _batidasService.InserirBatidaAsync(momento);

            if (responseResult.StatusCode == 201)
            {
                return Created("",responseResult.Content);
            }
            {
                int statusCode = responseResult.StatusCode;
                string errorMessage = responseResult.Content;
                return StatusCode(statusCode, new { message = errorMessage });
            }
        }

        [HttpGet("folhas-de-ponto/{mes}")]
        public async Task<IActionResult> ObterFolhaDePontoPorMes(string mes)
        {
            try
            {
                var folhaDePonto = await _folhasDePontoService.GerarRelatorioMensalAsync(mes);
                if (folhaDePonto == null)
                {
                    return NotFound();
                }
                return Ok(folhaDePonto);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
