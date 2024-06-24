using Commom;
using Commom.models;
using Entities.Entidades;
using Infra.Configuração;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Controller.MethodsCommom;
using WebApi.Email;
using WebApi.Token;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly ContextBase _context;
        private readonly IEmailService _emailService;
        public AuthController(ITokenService tokenService,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration,
                              ILogger<AuthController> logger,
                              ContextBase context,
                              IEmailService _EmailService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _emailService = _EmailService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Perfil adicionado");
                    return Ok($"Perfil {roleName} adicionado.");
                }
                else
                {
                    _logger.LogInformation(2, "Error");
                    return BadRequest($"Erro no perfil {roleName} ");
                }
            }
            return BadRequest($"Perfil {roleName} já cadastrado");
        }






        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                    return Ok($"Usuário {user.Email} adicionado ao perfil {roleName}.");
                }
                else
                {
                    _logger.LogInformation(1, $"Erro: Não foi possível adicionar {user.Email} para o perfil:{roleName}.");
                    return BadRequest($"Erro: Não foi possível adicionar {user.Email} para o perfil:{roleName}.");
                }
            }
            return BadRequest($"Não foi localizado o email: {email}");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Tenta encontrar o usuário pelo nome de usuário
                var user = await _userManager.FindByNameAsync(model.Username);

                // Tenta encontrar o usuário pelo CPF se não encontrou pelo nome de usuário
                if (user == null)
                {
                    user = await _userManager.Users.FirstOrDefaultAsync(u => u.CPF == model.Username);
                }

                // Se não encontrou o usuário por nenhum dos dois métodos, retorna "Usuário não cadastrado"
                if (user == null)
                {
                    return NotFound("Usuário não cadastrado.");
                }

                if (!user.EmailConfirmed)
                {
                    return Unauthorized($"Autorize sua conta no email enviado a:{user.Email}");
                }

                // Verifica a senha
                if (await _userManager.CheckPasswordAsync(user, model.Password!))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);


                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim(ClaimTypes.Email, user.Email!),
                        new Claim("id", user.UserName!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };


                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
                    var refreshToken = _tokenService.GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
                    user.RefreshToken = refreshToken;
                    await _userManager.UpdateAsync(user);

                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo
                    });
                }

                return Unauthorized("Senha incorreta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor. Tente novamente mais tarde.");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email!);

            if(userExists != null)
            {
                return BadRequest("Usuário já cadastrado no sistema.");
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CPF = model.Cpf,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if(!result.Succeeded)
            {
                return BadRequest($"Erro ao criar usuário {model.Username}. valor de result: {result.ToString()}");
            }

            //atribuindo ao perfil de usuário como DEFAULT ao criar o registro.
            await _userManager.AddToRoleAsync(user, "Usuário");

            // Gera o token de ativação
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email, "Confirme seu e-mail no natali-adv", $"Por favor, confirme sua conta clicando neste link: <a href='{confirmationLink}'>link</a>");
            return Ok($"Usuário {model.Username} Criado com sucesso");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Token expirado.");
            }

            string? accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));


            string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if(principal == null)
            {
                return BadRequest("Token de acesso/refresh Token , invalidos");
            }

            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username!);

            if(user == null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Token de acesso/refresh Token , invalidos");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken)
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return BadRequest("Nome de usuário inválido;");
            }
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok($"refresh token revogado de : {username}");
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("E-mail confirmado com sucesso!");
            }
            else
            {
                return BadRequest("Falha ao confirmar o e-mail.");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("confirmaemailmanual")]
        public async Task<IActionResult> ConfirmEmailManual(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("E-mail confirmado com sucesso!");
            }
            else
            {
                return BadRequest("Falha ao confirmar o e-mail.");
            }
        }
    }

}
