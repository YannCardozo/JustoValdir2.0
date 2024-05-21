using Commom.models.Usuarios;
using Entities.Entidades;
using Justo.Entities.Entidades;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace JustoFront.Services
{
    public interface IUsuarioService
    {
        Task<HttpResponseMessage> CreateUsuarioAsync(UsuarioComRoleSenha user);
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


        public async Task<HttpResponseMessage> CreateUsuarioAsync(UsuarioComRoleSenha user)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("api/Auth/register", user);

                // A verificação do status da resposta é feita aqui.
                if (!response.IsSuccessStatusCode)
                {
                    // Você pode logar a resposta de erro ou tratar de outra forma.
                    Console.WriteLine("Erro ao registrar usuário: " + response.StatusCode);
                }

                // Retorna a resposta independente do status ser sucesso ou não.
                return response;
            }
            catch (Exception ex)
            {
                // Loga a exceção e lança novamente ou trata de outra forma conforme necessário.
                Console.WriteLine("Erro ao enviar a requisição: " + ex.Message);
                throw; // Rethrowing the exception to handle it up the call stack.
            }
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
