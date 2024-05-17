using System.ComponentModel.DataAnnotations;

namespace Commom.models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Senha obrigatória")]
        public string? Password { get; set; }
    }
}
