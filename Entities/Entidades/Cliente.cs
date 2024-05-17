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
    [Table("Cliente")]
    public class Cliente : Base
    {

        public string? Nome { get; set; }
        public string? NomeMae { get; set; }

       
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        
        public string? ComprovanteDeResidencia { get; set; }
        public string? Cnh { get; set; }
        public string? ContratoSocialCliente { get; set; }
        public string? Cnpj { get; set; }
        public string? CertificadoReservista { get; set; }
        public string? ProcuracaoRepresentacaoLegal { get; set; }
        public string? PisPasep { get; set; }
        public string? CodClt { get; set; }
        public string? NIS { get; set; }

        public string? Genero { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Ocupacao { get; set; }
        public string? Renda { get; set; }
        public string? Escolaridade { get; set; }
        public string? Nacionalidade { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Banco { get; set; }
        public string? AgenciaBancaria { get; set; }
        public string? ContaCorrente { get; set; }
        public string? Telefone { get; set; }
        public string? Contato { get; set; }
        public string? Email { get; set; }
        
        public string? Tipo { get; set; }
        public string? ReuAutor { get; set; }

        [ForeignKey("Endereco")]
        [Column(Order = 1)]
        public int? EnderecoId { get; set; }
        public virtual Endereco? Endereco { get; set; }


        [ForeignKey("Processo")]
        [Column(Order = 2)]
        public int? ProcessoId { get; set; }
        public virtual Processo? Processo { get; set; }

    }
}
