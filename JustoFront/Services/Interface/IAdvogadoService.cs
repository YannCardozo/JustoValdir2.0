using Commom.models.Advogados;
using Justo.Entities.Entidades;

namespace JustoFront.Services.Interface
{
    public interface IAdvogadoService
    {
        Task<List<AdvogadoDTO>> GetAdvogadosAsync();
        Task<HttpResponseMessage> CreateAdvogadoAsync(AdvogadoDTO advogado);
        Task<HttpResponseMessage> UpdateAdvogadoAsync(AdvogadoDTO model);
    }
}
