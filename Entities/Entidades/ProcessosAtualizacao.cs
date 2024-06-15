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
    [Table("ProcessosAtualizacao")]
    public class ProcessosAtualizacao : Base
    {
        public string CodPJEC { get; set; }
        public string? PJECAcao { get; set; }
        public string? ConteudoAtualizacao { get; set; }
        public string? TituloMovimento { get; set; }
        public DateTime? DataMovimentacao { get; set; }

        [ForeignKey("Processo")]
        [Column(Order = 1)]
        public int ProcessoId {get;set;}
        public Processo Processo { get; set;}
    }
}
