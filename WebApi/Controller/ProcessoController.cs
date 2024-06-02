using Infra.Configuração;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        private readonly ContextBase _context;

        public ProcessoController(ContextBase context)
        {
            _context = context;
        }
        //api destinada a processos e suas atualizações

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("GetAllProcessos")]
        public async Task<IActionResult> GetAllProcessos()
        {
            try
            {
                var ListaProcessos = await _context.Processo.ToListAsync();
                if(ListaProcessos != null && ListaProcessos.Count > 0)
                {
                    return Ok(ListaProcessos);
                }
                else
                {
                    return NotFound("Sem processos cadastrados na base.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest($"Erro no servidor: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("GetProcessoById/{codPJEC}")]
        public async Task<IActionResult> GetProcessoById(string codPJEC)
        {
            try
            {
                var Processo = await _context.Processo.FirstOrDefaultAsync(u => u.CodPJEC == codPJEC);
                if (Processo != null)
                {
                    return Ok(Processo);
                }
                else
                {
                    return NotFound($"Processo: {codPJEC} não encontrado na base.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest($"Erro no servidor de processos: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("GetAllProcessosAtualizacao/{processoId}")]
        public async Task<IActionResult> GetAllProcessosAtualizacao(int processoId)
        {
            try
            {
                var ListaProcessosAtualizada = await _context.ProcessosAtualizacao.Where(u => u.ProcessoId == processoId).ToListAsync();
                if (ListaProcessosAtualizada == null || !ListaProcessosAtualizada.Any())
                {
                    return NotFound("Não foram encontradas atualizações para o processo especificado.");
                }
                else
                {
                    return Ok(ListaProcessosAtualizada);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro no servidor: {ex.Message}");
            }
        }



    }
}
