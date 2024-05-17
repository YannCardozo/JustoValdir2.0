using Domain.Interfaces.Generics;
using Justo.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IAdvogado
{
    public interface InterfaceAdvogado : InterfaceGeneric<Advogado>
    {
        Task<IList<Advogado>> ListarProcessosAdvogado(int CodProc);

    }
}
