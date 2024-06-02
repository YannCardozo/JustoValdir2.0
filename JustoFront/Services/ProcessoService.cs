using Justo.Entities.Entidades;
using JustoFront.Services.Interface;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace JustoFront.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ProcessoService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<List<Processo>> GetAllProcessosAsync()
        {
            var url = "api/Processo/GetAllProcessos";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Processo>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    await _jsRuntime.InvokeVoidAsync("alert", $"Erro ao obter processos: {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Exceção ao obter processos: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProcessosAtualizacao>> GetAllProcessoAtualizadoAsync(int processoId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Processo/GetAllProcessosAtualizacao/{processoId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ProcessosAtualizacao>>();
            }
            catch(Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Exceção ao obter processos: {ex.Message}");
                return null;
            }
        }
    }

}
