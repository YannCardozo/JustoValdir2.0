using Commom.models.Usuarios;
using Entities.Entidades;
using Infra.Configuração;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;




[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsuariosController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [AllowAnonymous]
    [HttpPut]
    [Route("UpdateUsuario")]
    public async Task<IActionResult> UpdateUsuario(UsuarioComRole usuarioAtualizado)
    {
        try
        {
            // Busca o usuário no banco de dados
            var usuario = await _userManager.FindByIdAsync(usuarioAtualizado.Id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Verificação de conflitos de UserName
            if (usuario.UserName != usuarioAtualizado.UserName)
            {
                var userNameExistente = await _userManager.FindByNameAsync(usuarioAtualizado.UserName);
                if (userNameExistente != null)
                {
                    return Conflict($"O nome de usuário '{usuarioAtualizado.UserName}' já está em uso.");
                }
                usuario.UserName = usuarioAtualizado.UserName;
            }
            else
            {
                usuario.UserName = usuarioAtualizado.UserName;
            }

            // Verificação de conflitos de Email
            if (usuario.Email != usuarioAtualizado.Email)
            {
                var emailExistente = await _userManager.FindByEmailAsync(usuarioAtualizado.Email);
                if (emailExistente != null)
                {
                    return Conflict($"O email '{usuarioAtualizado.Email}' já está em uso.");
                }
                usuario.Email = usuarioAtualizado.Email;
            }
            else
            {
                usuario.Email = usuarioAtualizado.Email;
            }

            // Atualização do CPF
            if (usuario.CPF != usuarioAtualizado.CPF)
            {
                // Supondo que você tenha uma verificação adicional para CPF único
                var cpfExistente = _userManager.Users.FirstOrDefault(u => u.CPF == usuarioAtualizado.CPF && u.Id != usuario.Id);
                if (cpfExistente != null)
                {
                    return Conflict($"O CPF '{usuarioAtualizado.CPF}' já está em uso.");
                }
                usuario.CPF = usuarioAtualizado.CPF;
            }
            else
            {
                usuario.CPF = usuarioAtualizado.CPF;
            }

            // aqui atualiza as informações do usuario, iremos para o role agora.
            var resultado = await _userManager.UpdateAsync(usuario);
            if (!resultado.Succeeded)
            {
                return BadRequest("Erro ao atualizar usuário: " + string.Join(", ", resultado.Errors.Select(e => e.Description)));
            }

            // Atualiza as roles do usuário
            var rolesAtuais = await _userManager.GetRolesAsync(usuario);
            var rolesParaAdicionar = usuarioAtualizado.Roles.Except(rolesAtuais).ToList();
            var rolesParaRemover = rolesAtuais.Except(usuarioAtualizado.Roles).ToList();

            if (rolesParaAdicionar.Any())
            {
                var resultadoAdicionar = await _userManager.AddToRolesAsync(usuario, rolesParaAdicionar);
                if (!resultadoAdicionar.Succeeded)
                {
                    return BadRequest("Erro ao adicionar roles: " + string.Join(", ", resultadoAdicionar.Errors.Select(e => e.Description)));
                }
            }

            if (rolesParaRemover.Any())
            {
                var resultadoRemover = await _userManager.RemoveFromRolesAsync(usuario, rolesParaRemover);
                if (!resultadoRemover.Succeeded)
                {
                    return BadRequest("Erro ao remover roles: " + string.Join(", ", resultadoRemover.Errors.Select(e => e.Description)));
                }
            }

            return Ok("Usuário atualizado com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetUsuarios")]
    public async Task<IActionResult> GetUsuarios()
    {
        try
        {
            var usuarios = _userManager.Users.ToList();
            if (usuarios == null )
            {
                return NotFound("Sem usuários Cadastrados");
            }

            var usuariosComRoles = new List<UsuarioComRole>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuariosComRoles.Add(new UsuarioComRole
                {
                    Id = usuario.Id,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    CPF = usuario.CPF,
                    Roles = roles
                });
            }

            return Ok(usuariosComRoles);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (roles == null || !roles.Any())
            {
                return NotFound("Sem Perfis cadastrados");
            }

            return Ok(roles);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
        }
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("DeleteUsuario/{cpf}")]
    public async Task<IActionResult> DeleteUsuario(string cpf)  // Usando string pois IDs do Identity geralmente são strings
    {
        try
        {
            var users = _userManager.Users.ToList();

            var user = users.FirstOrDefault(u => u.CPF == cpf);
            if (user == null)
            {
                return NotFound($"Usuário com CPF {cpf} não encontrado.");
            }

            // Deleta o usuário
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok($"Usuário com CPF {cpf} foi deletado com sucesso.");
            }
            else
            {
                return BadRequest($"Erro ao deletar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        catch (Exception ex)
        {
            // Retorna o erro no formato adequado
            return StatusCode(500, $"Erro interno ao deletar o usuário: {ex.Message}");
        }
    }


}