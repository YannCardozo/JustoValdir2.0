using Entities.Entidades;
using JustoNovo.Domain.ProcessosEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustoNovo.Domain.ProcessosEntidades
{
    public class SiteContato : Base
    {      
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public string TipoCausaContato { get; set; }        
        public bool AnalisadoContato { get; set; }
       
    }              

}
