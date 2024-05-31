using Commom.models.Advogados;
using Justo.Entities.Entidades;

namespace JustoFront.Services.Interface
{
    public interface IProcessoService
    {
        Task<List<Processo>> GetAllProcessosAsync();
        //Task<List<Processo>> GetAdvogadosComIdAsync();
        //Task<HttpResponseMessage> CreateAdvogadoAsync(Processo advogado);
        //Task<HttpResponseMessage> UpdateAdvogadoAsync(Processo model);
        //Task<HttpResponseMessage> DeleteAdvogadoAsync(int id);
        //Task<string> VerificarAdvogadoAsync(Processo model, bool? isUpdate);
    }
}
