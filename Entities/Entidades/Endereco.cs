using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justo.Entities.Entidades
{
    [Table("Endereco")]
    public class Endereco : Base
    {      

        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Municipio { get; set; }
        public string? UF { get; set; }
        public string? Cep { get; set; }
        public string? Referencia { get; set; }

        [ForeignKey("Cliente")]
        [Column(Order = 1)]
        public int? ClienteId { get; set; }
        public virtual Cliente? EnderecoCliente { get; set; }
    }
}
