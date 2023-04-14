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

        /// <summary>
        /// Registra um horário da jornada diária de trabalho
        /// </summary>
        /// <param name="momento">Momento da batida</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="409">Conflict</response>
        [ProducesResponseType(typeof(Registro), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status409Conflict)]
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
        /// <summary>
        /// Geração de relatório mensal de usuário.
        /// </summary>
        /// <param name="mes">Mês de referência</param>
        /// <response code="200">Relatório mensal</response>
        /// <response code="404">Relatório não encontrado</response>
        [ProducesResponseType(typeof(Relatorio), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]
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
