using Commom.models.Usuarios;
using Entities.Entidades;
using Justo.Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
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
        Task<List<ApplicationUser>> GetUsuariosDoIdentity();
        Task<List<string>> GetRolesAsync();
        Task<HttpResponseMessage> UpdateUserAsync(UsuarioComRole usuarionovo);
        //Task<HttpResponseMessage> UpdateUserAsync(UsuarioComRole usuario);
        Task<HttpResponseMessage> DeleteUserAsync(string id);
        Task<ApplicationUser> GetUsuarioByCpfAsync(string cpf);

    }

    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        IJSRuntime JSRuntime;
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
                    await JSRuntime.InvokeVoidAsync("alert", response.Content.ReadAsStringAsync());
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
        public async Task<HttpResponseMessage> DeleteUserAsync(string cpf)
        {
            // Constrói corretamente o URI com o ID
            var url = $"api/Usuarios/DeleteUsuario/{cpf}";

            // Faz a chamada usando HttpClient.DeleteAsync, que é apropriado para requisições DELETE
            var response = await _httpClient.DeleteAsync(url);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateUserAsync(UsuarioComRole usuarionovo)
        {
            var url = "api/Usuarios/UpdateUsuario";

            var response = await _httpClient.PutAsJsonAsync(url, usuarionovo);

            if(response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                return response;
            }
            
        }
        public async Task<ApplicationUser> GetUsuarioByCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio ou nulo.");

            // Define a URL do endpoint que retorna a lista de usuários
            var url = "api/Usuarios/GetUsuarios";

            try
            {
                // Faz a chamada para a API que retorna todos os usuários
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Deserializa a resposta em uma lista de usuários
                    var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();

                    Console.WriteLine(users.FirstOrDefault(u => u.CPF == cpf));
                    Console.WriteLine(cpf);
                    if (users != null)
                    {
                        // Procura na lista o usuário com o CPF especificado
                        return users.FirstOrDefault(u => u.CPF == cpf);
                    }
                }
                else
                {
                    // Tratamento de erro, se a API retornar um status de erro
                    var error = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Erro ao buscar usuários: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Tratamento de erro de requisição
                throw new ApplicationException($"Erro de conexão ao servidor: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Erro na deserialização do JSON
                throw new ApplicationException($"Erro ao deserializar a lista de usuários: {ex.Message}");
            }

            return null;
        }

    }

}
