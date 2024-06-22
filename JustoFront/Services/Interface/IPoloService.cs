using Justo.Entities.Entidades;

namespace JustoFront.Services.Interface
{
    public interface IPoloService
    {
        Task<List<Polo>> ListarPolos();
        Task<List<Polo>> GetPoloByProcessoId(int ProcessoId);
        Task<HttpResponseMessage> DeletarPolo(int PoloId);

    }
}
