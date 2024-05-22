using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commom.models.Validations;
using Entities.Entidades;
using Justo.Entities.Entidades;
using static Commom.models.Validations.AdvogadoValidation;

namespace Commom.models.Advogados
{
    public class AdvogadoDTO : Base
    {
        [Required(ErrorMessage = "Advogado Precisa de um Nome")]
        public string Nome { get; set; }
        [ValidaOab(ErrorMessage = "O número da OAB deve estar no formato '123456-UF'.")]
        public string? Oab { get; set; }
        [Required(ErrorMessage = "Advogado precisa de um CPF")]
        [ValidaCPF(ErrorMessage = "CPF do Advogado Inválido")]
        public string Cpf { get; set; }
    }
}
