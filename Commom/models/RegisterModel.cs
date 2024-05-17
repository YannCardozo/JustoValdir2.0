using System.ComponentModel.DataAnnotations;

namespace Commom.models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatória")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "CPF obrigatório")]
        public string Cpf { get; set; }
    }
}
