using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades
{
    public class Base
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }
        public int CadastradoPor { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int AtualizadoPor { get; set; }
    }
}
