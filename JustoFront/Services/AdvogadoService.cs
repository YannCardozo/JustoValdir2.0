using Commom.models.Advogados;
using Justo.Entities.Entidades;
using JustoFront.Services.Interface;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


public class AdvogadoService : IAdvogadoService
{
    private readonly HttpClient _httpClient;
    private Advogado ModelASerCriada = new();

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

    public async Task<HttpResponseMessage> CreateAdvogadoAsync(AdvogadoDTO model)
    {
        HttpResponseMessage response;
        var ListaAdvogados = await GetAdvogadosAsync();

        // Verifica se o advogado já existe na lista obtida da API
        if (ListaAdvogados != null && ListaAdvogados.Any(advogado => advogado.Cpf == model.Cpf))
        {
            // Se o advogado já existe, você pode lidar com isso aqui.
            // Por exemplo, retornar uma mensagem de erro ou fazer outra coisa.
            Console.WriteLine("Advogado já existe.");
            return null; // Ou lance uma exceção ou faça outro tratamento, dependendo do seu caso.
        }

        // Se o advogado não existe, você pode prosseguir com a lógica para criar o advogado.
        // Aqui você pode chamar outra função para enviar o advogado para a API, por exemplo.
        // Ou você pode implementar o código para criar o advogado diretamente aqui.


        ModelASerCriada.Nome = model.Nome;
        ModelASerCriada.Cpf = model.Cpf;
        ModelASerCriada.Oab = model.Oab;



        response =  await _httpClient.PostAsJsonAsync("api/Advogado/CreateAdvogado", ModelASerCriada);

        return response; // Retornar o advogado criado
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