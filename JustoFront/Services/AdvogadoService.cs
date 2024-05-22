using Commom.models.Advogados;
using Justo.Entities.Entidades;
using JustoFront.Services.Interface;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


public class AdvogadoService : IAdvogadoService
{
    private readonly HttpClient _httpClient;

    public AdvogadoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AdvogadoDTO>> GetAdvogadosAsync()
    {
        var response = await _httpClient.GetAsync("api/Advogado/ListarAdvogados");
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var advogados = await response.Content.ReadFromJsonAsync<List<Advogado>>();
                return ConvertToDTO(advogados);
            }
            catch (JsonException ex)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Exception: {ex.Message}, Content: {errorContent}");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
        }
        return new List<AdvogadoDTO>();
    }

    public List<AdvogadoDTO> ConvertToDTO(List<Advogado> advogados)
    {
        return advogados.ConvertAll(a => new AdvogadoDTO
        {
            Nome = a.Nome,
            Oab = a.Oab,
            Cpf = a.Cpf
            // Adicionar outros campos conforme necessário
        });
    }
























    //public async Task<List<Advogado>> GetAdvogadosAsync()
    //{
    //    var response = await _httpClient.GetAsync("api/Advogado/ListarAdvogados");
    //    if (response.IsSuccessStatusCode)
    //    {
    //        try
    //        {
    //            return await response.Content.ReadFromJsonAsync<List<Advogado>>();
    //        }
    //        catch (JsonException ex)
    //        {
    //            var errorContent = await response.Content.ReadAsStringAsync();
    //            Console.WriteLine($"JSON Exception: {ex.Message}, Content: {errorContent}");
    //        }
    //    }
    //    else
    //    {
    //        var errorContent = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
    //    }
    //    return new List<Advogado>();
    //}
}