using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commom.models.Usuarios
{
    public class UsuarioComRoleSenha
    {
        private const string PasswordAndUsernamePattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$";

        private const string UsernamePattern = @"^[a-zA-Z0-9]+$";

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [RegularExpression(UsernamePattern, ErrorMessage = "O nome de usuário deve conter apenas letras ( maiúsculas e minúsculas ) e números.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [CPFValidationT(ErrorMessage = "O CPF não é válido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório.")]
        public string RoleSelecionado { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [RegularExpression(PasswordAndUsernamePattern, ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string Password { get; set; }
    }

    public class CPFValidationT : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = value as string;
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = Regex.Replace(cpf, "[^0-9]", string.Empty);

            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            var tempCpf = cpf.Substring(0, 9);
            var sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * (10 - i);

            var remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            var digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();
            return cpf.EndsWith(digit);
        }
    }
}
