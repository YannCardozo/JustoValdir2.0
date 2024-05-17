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
    [Table("AdvogadoEspecialidade")]
    public class AdvogadoEspecialidade : Base
    {
        public string? NomeAreaDireito { get; set; }

        [ForeignKey("Advogado")]
        [Column(Order = 1)]

        public int? AdvogadoId { get; set; }     
        public virtual Advogado? Advogado { get; set; } = new Advogado();


    }
}
