using Commom.models.Advogados;
using Commom.models.Usuarios;
using Entities.Entidades;
using Infra.Configuração;
using Justo.Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controller.MethodsCommom;
using WebApi.Models;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvogadoController : ControllerBase
    {
        private Advogado AdvogadoAtualizado { get; set; } = new();
        private readonly ContextBase _context;

        public AdvogadoController(ContextBase context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("ListarAdvogados")]
        public async Task<IActionResult> ListarAdvogados()
        {
            try
            {
                var advogados = await _context.Advogado.ToListAsync();

                if (advogados == null || advogados.Count == 0)
                {
                    return Ok("Sem Advogados cadastrados no sistema.");
                }

                return Ok(advogados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ErrorResponse>
                {
                    Data = new ErrorResponse { ErrorMessage = "Erro ao buscar advogados no sistema." }
                });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("UpdateAdvogado")]
        public async Task<IActionResult> UpdateAdvogado([FromBody] AdvogadoDTO model)
        {
            try
            {
                var advogadobusca = await _context.Advogado.FindAsync(model.Id);

                if (advogadobusca == null)
                {
                    return NotFound(new { Message = $"Advogado com ID {model.Id} não encontrado." });
                }

                // Atualizar propriedades do advogado
                advogadobusca.Cpf = model.Cpf;
                advogadobusca.Nome = model.Nome;
                advogadobusca.Oab = model.Oab;

                _context.Advogado.Update(advogadobusca);
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"Advogado com CPF {model.Cpf} atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Erro ao atualizar advogado no sistema.", Error = ex.Message });
            }
        }



        //[Authorize(Policy = "AdminOnly")]]
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpDelete("DeleteAdvogados/{id}")]
        public async Task<IActionResult> DeleteAdvogados([FromRoute] int id)
        {
            try
            {
                var advogado = await _context.Advogado.FindAsync(id);
                if (advogado == null)
                {
                    // Melhoria na mensagem de retorno para ser mais específica sobre o erro.
                    return NotFound($"Advogado não encontrado. o valor do id é: {id}");
                }

                _context.Advogado.Remove(advogado);
                await _context.SaveChangesAsync();

                return Ok("Advogado removido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao remover o advogado. " + ex.Message);
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("CreateAdvogado")]
        public async Task<IActionResult> CreateAdvogado(AdvogadoDTO model)
        {
            try
            {
                // Verifica se já existe um advogado com o mesmo CPF
                var advogadoExistenteCpf = await _context.Advogado.FirstOrDefaultAsync(x => x.Cpf == model.Cpf);
                if (advogadoExistenteCpf != null)
                {
                    return BadRequest($"Advogado com CPF {model.Cpf} já está cadastrado no sistema.");
                }

                // Verifica se já existe um advogado com a mesma OAB
                var advogadoExistenteOab = await _context.Advogado.FirstOrDefaultAsync(x => x.Oab == model.Oab);
                if (advogadoExistenteOab != null)
                {
                    return BadRequest($"Advogado com OAB {model.Oab} já está cadastrado no sistema.");
                }

                // Cria o advogado se não houver erro
                var AdvogadoASerCriado = new Advogado
                {
                    Cpf = model.Cpf,
                    Nome = model.Nome,
                    Oab = model.Oab
                };

                _context.Advogado.Add(AdvogadoASerCriado);
                await _context.SaveChangesAsync();

                return Ok($"Advogado {AdvogadoASerCriado.Nome} foi criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar o advogado: {ex.Message}");
            }
        }

    }
}
