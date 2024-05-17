using Entities.Entidades;
using Infra.Configuração;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using WebApi.Controller.MethodsCommom;
using WebApi.Models;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ContextBase _context; // Adicione o contexto do banco de dados

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ContextBase context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]

        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            var cpfExistente = await VerifyDB.VerificarCpfExistente(_context, login.Cpf);
            if (cpfExistente)
            {
                return NotFound(new ApiResponse<ErrorResponse>
                {
                    Data = $"CPF {login.Cpf} já está cadastrado no sistema."
                });
            }
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password) || string.IsNullOrEmpty(login.Cpf))
            {
                return Ok("Falta alguns dados");


            }
            var user = new ApplicationUser
            {
                Email = login.Email,
                UserName = login.Email,
                CPF = login.Cpf
            };

            var result = await _userManager.CreateAsync(user, login.Password);

            if(result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }

            // Geração de confirmação de email
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno do email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var respose_Retorn = await _userManager.ConfirmEmailAsync(user, code);

            if (respose_Retorn.Succeeded)
            {
                return Ok("Usuário Adicionado!");
            }
            else
            {
                return BadRequest("erro ao confirmar cadastro de usuário!");
            }
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario([FromBody] Login login)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.CPF == login.Cpf);

            if(user == null)
            {
                return NotFound(new ApiResponse<ErrorResponse>
                {
                    Data = $"CPF {login.Cpf} Não localizado no sistema."
                });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new ApiResponse<SuccessResponse>
            {
                Data = $"Usuário {login.Email} removido com sucesso."
            });
        }


        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/ListaUsuario")]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                // Recuperar todos os usuários da base de dados
                var usuarios = await _userManager.Users.ToListAsync();

                // Se não houver usuários, retornar um 404
                if (usuarios == null || usuarios.Count == 0)
                    return NotFound();

                // Extrair apenas os e-mails dos usuários
                var emails = usuarios.Select(u => u.Email).ToList();

                // Retornar a lista de e-mails dos usuários
                return Ok(emails);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retornar um 500 com uma mensagem de erro
                return StatusCode(500, $"Não há usuários cadastrados: {ex.Message}");
            }
        }



    }
}
