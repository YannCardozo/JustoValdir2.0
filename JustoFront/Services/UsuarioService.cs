using Commom.models.Usuarios;
using Entities.Entidades;
using Justo.Entities.Entidades;
using System.Net.Http.Json;
using System.Text.Json;

namespace JustoFront.Services
{
    public interface IUsuarioService
    {
        Task<HttpResponseMessage> CreateUsuarioAsync(UsuarioComRole user);
        Task<List<UsuarioComRole>> GetUsuariosAsync();
        Task<List<string>> GetRolesAsync();
        Task<HttpResponseMessage> UpdateUserAsync(UsuarioComRole usuario);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResponseMessage> CreateUsuarioAsync(UsuarioComRole user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", user);
            return response;
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

        public async Task<List<string>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync("api/Usuarios/GetRoles");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<List<string>>();
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

        public async Task<HttpResponseMessage> UpdateUserAsync(UsuarioComRole usuario)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Usuarios/UpdateUser", usuario);
            return response;
        }
    }

}
