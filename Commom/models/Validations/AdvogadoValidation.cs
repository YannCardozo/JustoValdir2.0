using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commom.models.Validations
{
    public static class AdvogadoValidation
    {
        public class ValidaCPF : ValidationAttribute
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

        public class ValidaOab : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is string oabNumber)
                {
                    if (Regex.IsMatch(oabNumber, @"^\d{5,6}-[A-Z]{2}$"))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("O número da OAB deve estar no formato '123456-UF'.");
                    }
                }
                return new ValidationResult("O número da OAB é inválido.");
            }
        }
    }
}
