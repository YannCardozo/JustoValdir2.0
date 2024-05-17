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
    [Table("ProcessosCompromissos")]
    public class ProcessosCompromissos : Base
    {        
        public string Nome { get; set; }
        public string NomeAdvogado { get; set; }
        public string NomeCliente { get; set; }
        public DateTime Data { get; set; }
        public string Obs { get; set; }
        public string Local { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public DateTime DataProximoEvento { get; set; }

        [ForeignKey("Processo")]
        [Column(Order = 1)]
        public int ProcessoId { get; set; }
        public Processo Processo { get; set; }
    }
}
