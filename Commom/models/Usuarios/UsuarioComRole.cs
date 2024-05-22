using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Commom.models.Validations;
using static Commom.models.Validations.UsuarioValidation;

namespace Commom.models.Usuarios
{
    public class UsuarioComRole
    {

        public IList<string> Roles { get; set; }


        private const string PasswordAndUsernamePattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$";

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [RegularExpression(PasswordAndUsernamePattern, ErrorMessage = "O nome de usuário deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [ValidaCPF(ErrorMessage = "O CPF não é válido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório.")]
        public string RoleSelecionado { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [RegularExpression(PasswordAndUsernamePattern, ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string Password { get; set; }
    }
}

