using Domain.Interfaces.Generics;
using Domain.Interfaces.IAtualizacao;
using Infra.Repositório.Generics;
using Justo.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositório
{
    public class RepositorioAtualizacao : RepositoryGenerics<ProcessosAtualizacao>, InterfaceAtualizacao
    { 

    }
}
