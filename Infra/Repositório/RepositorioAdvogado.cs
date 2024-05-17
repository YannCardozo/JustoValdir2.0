using Domain.Interfaces.IAdvogado;
using Infra.Configuração;
using Infra.Repositório.Generics;
using Justo.Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositório
{
    public class RepositorioAdvogado : RepositoryGenerics<Advogado>, InterfaceAdvogado
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositorioAdvogado()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Advogado>> ListarProcessosAdvogado(int CodProc)
        {
            //using (var banco = new ContextBase(_OptionsBuilder))
            //{
            
            //return await
            //        (from s in banco.Advogado
            //         join c in banco.Processo on s.Id equals c.AdvogadoId
            //         select )
            //}
            
            throw new NotImplementedException();
        }
    }
}
