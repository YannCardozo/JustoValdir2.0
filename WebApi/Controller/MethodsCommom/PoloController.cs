using Commom.models.Advogados;
using Infra.Configuração;
using Justo.Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controller.MethodsCommom
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoloController : ControllerBase
    {
        private Polo PoloAtualizado { get; set; } = new();
        private readonly ContextBase _context;

        public PoloController(ContextBase context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("ListarPolos")]
        public async Task<IActionResult> ListarPolos()
        {
            try
            {
                var Polos = await _context.Polo.ToListAsync();

                if (Polos == null || Polos.Count == 0)
                {
                    return Ok("Sem Polos cadastrados no sistema.");
                }

                return Ok(Polos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ErrorResponse>
                {
                    Data = new ErrorResponse { ErrorMessage = "Erro ao buscar Polos no sistema." }
                });
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("BuscarPoloPorProcessoId/{id}")]
        public async Task<IActionResult> BuscarPoloPorProcessoId([FromRoute] int id)
        {
            //método para buscar polo a partir do processoid
            try
            {
                var PoloComProcessoId = await _context.Polo.FirstOrDefaultAsync(u => u.ProcessoId == id);

                if (PoloComProcessoId == null)
                {
                    return Ok($"Polo:{id} não encontrado.");
                }

                return Ok(PoloComProcessoId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ErrorResponse>
                {
                    Data = new ErrorResponse { ErrorMessage = $"Erro ao buscar Polo de ID:{id}" }
                });
            }
        }


        //[Authorize(Policy = "AdminOnly")]]
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpDelete("DeletarPolo/{id}")]
        public async Task<IActionResult> DeletarPolo([FromRoute] int id)
        {
            try
            {
                var Polo = await _context.Polo.FindAsync(id);
                if (Polo == null)
                {
                    return NotFound($"Polo não encontrado. o valor do id é: {id}");
                }

                _context.Polo.Remove(Polo);
                await _context.SaveChangesAsync();
                return Ok("Polo removido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao remover o Polo. " + ex.Message);
            }
        }

    }
}
