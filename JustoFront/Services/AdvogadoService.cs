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
    private Advogado ModelASerAtualizada = new();
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

    public async Task<List<Advogado>> GetAdvogadosComIdAsync()
    {
        var response = await _httpClient.GetAsync("api/Advogado/ListarAdvogados");
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var advogados = await response.Content.ReadFromJsonAsync<List<Advogado>>();
                return advogados ?? new List<Advogado>(); // Retorna uma lista vazia se null for retornado.
            }
            catch (JsonException ex)
            {
                // Log the JSON error and the response content for troubleshooting.
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Exception: {ex.Message}, Content: {errorContent}");
            }
        }
        else
        {
            // Log error information when the response is not successful.
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
        }

        // Return an empty list if the HTTP request fails or JSON parsing fails.
        return new List<Advogado>();
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


        Console.WriteLine("ID de model é: " + model.Id + "    id de modelasercriada é: " + ModelASerCriada.Id);

        response =  await _httpClient.PostAsJsonAsync("api/Advogado/CreateAdvogado", ModelASerCriada);
        Console.WriteLine("ID de model é: " + model.Id + "    id de modelasercriada é: " + ModelASerCriada.Id);

        return response; // Retornar o advogado criado
    }

   
    public async Task<HttpResponseMessage> DeleteAdvogadoAsync(int id)
    {
        Console.WriteLine("o valor do id é: " + id);
        return await _httpClient.DeleteAsync($"api/Advogado/DeleteAdvogados/{id}");
    }




    public async Task<HttpResponseMessage> UpdateAdvogadoAsync(AdvogadoDTO model)
    {
        var ListaAdvogados = await GetAdvogadosAsync();

        if (ListaAdvogados != null && ListaAdvogados.Any(advogado => advogado.Cpf == model.Cpf))
        {
            var advogadoExistente = ListaAdvogados.FirstOrDefault(advogado => advogado.Cpf == model.Cpf);
            if (advogadoExistente != null)
            {
                model.Id = advogadoExistente.Id; // Certifique-se de atribuir o ID existente ao modelo DTO
                return await _httpClient.PutAsJsonAsync("api/Advogado/UpdateAdvogado", model);
            }
        }
        else
        {
            Console.WriteLine($"Advogado {model.Cpf} não localizado.");
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                ReasonPhrase = $"Advogado com CPF {model.Cpf} não localizado."
            };
        }

        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            ReasonPhrase = "Erro ao atualizar advogado."
        };
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
}