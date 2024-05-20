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
    [Route("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UsuarioComRole model)
    {
        try
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            if (string.IsNullOrEmpty(model.RoleSelecionado) || !roles.Contains(model.RoleSelecionado))
            {
                return BadRequest("Preencha o Perfil do usuário para continuar.");
            }

            // Verificar se o email já está em uso por outro usuário
            var userWithEmail = await _userManager.Users
                .Where(u => u.Email == model.Email && u.UserName != model.UserName)
                .SingleOrDefaultAsync();

            if (userWithEmail != null)
            {
                return BadRequest($"O email '{model.Email}' já está em uso.");
            }

            // Verificar se o username já está em uso por outro usuário
            var userWithUsername = await _userManager.Users
                .Where(u => u.UserName == model.UserName && u.UserName != model.UserName)
                .SingleOrDefaultAsync();

            if (userWithUsername != null)
            {
                return BadRequest($"O nome de usuário '{model.UserName}' já está em uso.");
            }

            // Verificar se o CPF já está em uso por outro usuário
            var userWithCpf = await _userManager.Users
                .Where(u => u.CPF == model.CPF && u.UserName != model.UserName)
                .SingleOrDefaultAsync();

            if (userWithCpf != null)
            {
                return BadRequest($"O CPF '{model.CPF}' já está em uso.");
            }

            // Encontrar o usuário pelo nome de usuário fornecido no model
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound($"Usuário com o nome de usuário '{model.UserName}' não encontrado.");
            }

            // Obter roles atuais do usuário
            var currentRoles = await _userManager.GetRolesAsync(user);
            var newRole = model.RoleSelecionado; // Assume que 'RoleSelecionado' é a nova role do usuário

            // Remover roles atuais
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest($"Erro ao remover roles atuais do usuário: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
            }

            // Adicionar nova role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
            {
                return BadRequest($"Erro ao adicionar nova role ao usuário: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
            }

            // Atualizar propriedades do usuário
            user.UserName = model.UserName;
            user.CPF = model.CPF;


            if (model.Email.Contains("@")) ;
            {
                user.Email = model.Email;
            }


            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest($"Erro ao atualizar o usuário: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }

            return Ok($"{user.UserName} atualizado com sucesso.");
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

}