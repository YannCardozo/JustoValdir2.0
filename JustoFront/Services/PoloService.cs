using Justo.Entities.Entidades;
using JustoFront.Services.Interface;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace JustoFront.Services
{
    public class PoloService : IPoloService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public PoloService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<List<Polo>> ListarPolos()
        {
            var url = "api/Polo/ListarPolos";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Polo>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    await _jsRuntime.InvokeVoidAsync("alert", $"Erro ao obter Polos: {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Exceção ao obter Polos: {ex.Message}");
                return null;
            }
        }
        public async Task<HttpResponseMessage> DeletarPolo(int PoloId)
        {
            try
            {
                return await _httpClient.DeleteAsync($"api/Polo/DeletarPolo/{PoloId}");

            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"erro poloservice deletar polo: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Polo>> GetPoloByProcessoId(int ProcessoId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Polo/GetAllProcessosAtualizacao/{ProcessoId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Polo>>();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Exceção ao obter Polo: {ex.Message}");
                return null;
            }
        }

    }
}
