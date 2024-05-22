using Commom.models.Advogados;
using Justo.Entities.Entidades;

namespace JustoFront.Services.Interface
{
    public interface IAdvogadoService
    {
        Task<List<AdvogadoDTO>> GetAdvogadosAsync();
    }
}
