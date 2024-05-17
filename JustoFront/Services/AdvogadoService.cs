using Justo.Entities.Entidades;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public interface IAdvogadoService
{
    Task<List<Advogado>> GetAdvogadosAsync();
}

public class AdvogadoService : IAdvogadoService
{
    private readonly HttpClient _httpClient;

    public AdvogadoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Advogado>> GetAdvogadosAsync()
    {
        var response = await _httpClient.GetAsync("api/Advogado/ListarAdvogados");
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<List<Advogado>>();
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
        return new List<Advogado>();
    }
}