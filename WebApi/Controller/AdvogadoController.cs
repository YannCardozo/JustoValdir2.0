﻿using Commom.models.Advogados;
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

        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
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

        [Authorize(Policy = "AdminOnly")]
        [Produces("application/json")]
        [HttpPost("DeleteAdvogados/{id}")]
        public async Task<IActionResult> DeleteAdvogados(int id)
        {
            try
            {
                var advogado = await _context.Advogado.FindAsync(id);
                if (advogado == null)
                {
                    return NotFound(new ApiResponse<ErrorResponse>
                    {
                        Data = $"Advogado não cadastrado no sistema (verificar ID)."
                    });
                }

                _context.Advogado.Remove(advogado);
                await _context.SaveChangesAsync();
                // Retornar a lista de e-mails dos usuários
                return Ok(new ApiResponse<string>
                {
                    Data = $"Advogado {advogado.Nome} com ID:{id} foi removido com sucesso."
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<ErrorResponse>
                {
                    Data = $"Sem advogados de ID:{id} cadastrados no sistema."
                });
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("CreateAdvogado")]
        public async Task<IActionResult> CreateAdvogado(AdvogadoDTO model)
        {
            try
            {
                var advogadoExistente = await _context.Advogado.FindAsync(model.Id);

                if (advogadoExistente != null)
                {
                    return Conflict(new ApiResponse<ErrorResponse>
                    {
                        Data = $"Advogado com ID {model.Id} já está cadastrado no sistema."
                    });
                }

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
