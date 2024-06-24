using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Email;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [AllowAnonymous]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok();
        }

        //[AllowAnonymous]
        //[HttpPost("cadastrar/{string}")]
        //public async Task<IActionResult> EmailCadastrarUsuario([FromBody] EmailRequest request)
        //{
        //    if(string.IsNullOrEmpty(request.Token))
        //    {

        //    }
        //}
    }


}
