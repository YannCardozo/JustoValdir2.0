using Commom.models.Advogados;
using Justo.Entities.Entidades;

namespace JustoFront.Services.Interface
{
    public interface IAdvogadoService
    {
        Task<List<AdvogadoDTO>> GetAdvogadosAsync();
        Task<List<Advogado>> GetAdvogadosComIdAsync();
        Task<HttpResponseMessage> CreateAdvogadoAsync(AdvogadoDTO advogado);
        Task<HttpResponseMessage> UpdateAdvogadoAsync(AdvogadoDTO model);
        Task<HttpResponseMessage> DeleteAdvogadoAsync(int id);
    }
}
