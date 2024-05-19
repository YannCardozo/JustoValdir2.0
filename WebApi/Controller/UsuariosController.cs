using Commom.models.Usuarios;
using Entities.Entidades;
using Infra.Configuração;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;




[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsuariosController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ContextBase _context;

    public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ContextBase context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [Authorize]
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
}