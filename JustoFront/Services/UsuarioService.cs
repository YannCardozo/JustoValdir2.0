using Commom.models.Usuarios;
using Entities.Entidades;
using Justo.Entities.Entidades;
using System.Net.Http.Json;
using System.Text.Json;

namespace JustoFront.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioComRole>> GetUsuariosAsync();
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioComRole>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync("api/Usuarios/GetUsuarios");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<List<UsuarioComRole>>();
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
            return null; // Retorna null em caso de falha
        }
    }
}
