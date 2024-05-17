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
    [Table("Processo")]
    public class Processo : Base
    {
        public string CodPJEC { get; set; }
        public string? ObsProcesso { get; set; }
        public DateTime? DataFim { get; set; }



        public string? MeioDeComunicacao { get; set; }
        public DateTime? MeioDeComunicacaoData { get; set; }
        public string? Prazo { get; set; }
        public string? ProximoPrazo { get; set; }
        public string? ProximoPrazoData { get; set; }
        public string? PJECAcao { get; set; }
        public string? UltimaMovimentacaoProcessual { get; set; }
        public DateTime? UltimaMovimentacaoProcessualData { get; set; }
        public string? AdvogadaCiente { get; set; }
        public string? Comarca { get; set; }
        public string? OrgaoJulgador { get; set; }
        public string? Competencia { get; set; }
        public string? MotivosProcesso { get; set; }
        public string? SegredoJustica { get; set; }
        public string? JusGratis { get; set; }
        public string? TutelaLiminar { get; set; }
        public string? Prioridade { get; set; }
        public string? Autuacao { get; set; }













        public string? TituloProcesso { get; set; }
        public string? PartesProcesso { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string? ValorDaCausa { get; set; }

        [ForeignKey("Advogado")]
        [Column(Order = 1)]

        public int AdvogadoId { get; set; }
        public Advogado Advogado { get; set; } = new();


        public ICollection<ProcessosCompromissos> ProcessosCompromissos = new List<ProcessosCompromissos>();
        //remapeado para que um advogado esteja para um processo apenas, 1:1

        public ICollection<ProcessosAtualizacao> ProcessosAtualizacoes { get; set; } = new List<ProcessosAtualizacao>();
        public ICollection<Polo> PoloPartes { get; set; } = new List<Polo>();



    }
}
