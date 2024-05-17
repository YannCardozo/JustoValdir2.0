using Domain.Interfaces.IContato;
using Infra.Repositório.Generics;
using JustoNovo.Domain.ProcessosEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositório
{
    public class RepositorioContato : RepositoryGenerics<SiteContato> , InterfaceContato
    {
    }
}
