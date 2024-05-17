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
    [Table("Polo")]
    public class Polo : Base
    {

        public string? NomeParte { get; set; }
        public string? TipoParte { get; set; } 
        public string? CPFCNPJParte { get; set; }
        public string? NomeAdvogado { get; set; }
        public string? CPFAdvogado { get; set; }
        public string? OAB { get; set; }

        [ForeignKey("Processo")]
        [Column(Order = 1)]
        public int ProcessoId { get; set; }
        public Processo Processo { get; set; } = new();
    }
}
