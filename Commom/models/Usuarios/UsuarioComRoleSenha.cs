using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commom.models.Usuarios
{
    public class UsuarioComRoleSenha
    {

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório.")]
        public string RoleSelecionado { get; set; }

        public string Password { get; set; }
    }
}
