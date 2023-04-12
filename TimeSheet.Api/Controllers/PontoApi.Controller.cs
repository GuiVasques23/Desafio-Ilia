using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Time.Sheet.Domain.Models;
using Microsoft.Win32;
using Time.Sheet.Domain.Services;

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
            try
            {
                var registro = await _batidasService.InserirBatidaAsync(momento);
                return CreatedAtAction(nameof(ObterRegistroPorId), new { id = registro.Id }, registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Mensagem { Texto = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("folhas-de-ponto/{mes}")]
        public async Task<IActionResult> ObterFolhaDePontoPorMes(DateTime mes)
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

        [HttpGet("registros/{id}")]
        public async Task<IActionResult> ObterRegistroPorId(int id)
        {
            try
            {
                var registro = await _batidasService.ObterRegistroPorIdAsync(id);
                if (registro == null)
                {
                    return NotFound();
                }
                return Ok(registro);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
