using Domain.Interfaces.ICompromisso;
using Infra.Repositório.Generics;
using Justo.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositório
{
    public class RepositorioCompromisso : RepositoryGenerics<ProcessosCompromissos> , InterfaceCompromisso
    {
    }
}
