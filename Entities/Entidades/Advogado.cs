using Entities.Entidades;
using JustoNovo.Domain.ProcessosEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justo.Entities.Entidades
{
    [Table("Advogado")]
    public class Advogado : Base
    {
        [Required]
        public string Nome { get; set; }
        public string? Oab { get; set; }
        [Required]
        public string Cpf { get; set; }
        public virtual ICollection<AdvogadoEspecialidade>? AdvogadosEspecialidades { get; set; } = new List<AdvogadoEspecialidade>();

        public ICollection<Processo>? Processos = new List<Processo>();
    }
}
