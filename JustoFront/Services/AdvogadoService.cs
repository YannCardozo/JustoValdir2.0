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
        var ListaAdvogados = await GetAdvogadosAsync();

        if (ListaAdvogados != null)
        {
            if (ListaAdvogados.Any(advogado => advogado.Cpf == model.Cpf))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Advogado com CPF {model.Cpf} já está cadastrado no sistema.")
                };
            }
            else if (ListaAdvogados.Any(advogado => advogado.Oab == model.Oab))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Advogado com OAB {model.Oab} já está cadastrado no sistema.")
                };
            }
        }

        var ModelASerCriada = new AdvogadoDTO
        {
            Nome = model.Nome,
            Cpf = model.Cpf,
            Oab = model.Oab
        };

        var response = await _httpClient.PostAsJsonAsync("api/Advogado/CreateAdvogado", ModelASerCriada);

        return response;
    }


    public async Task<HttpResponseMessage> DeleteAdvogadoAsync(int id)
    {
        Console.WriteLine("o valor do id é: " + id);
        return await _httpClient.DeleteAsync($"api/Advogado/DeleteAdvogados/{id}");
    }




    public async Task<HttpResponseMessage> UpdateAdvogadoAsync(AdvogadoDTO model)
    {
        var verificamodeldenovo = await VerificarAdvogadoAsync(model, true);

        if (verificamodeldenovo == null)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Advogado/UpdateAdvogado", model);
            if (response.IsSuccessStatusCode)
            {
                return response;  // Retorna a resposta de sucesso
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = $"Erro ao atualizar advogado: {response.ReasonPhrase}"
                };
            }
        }
        else
        {
            Console.WriteLine($"Verificar os dados do Advogado: {model.Cpf} / {model.Oab} / {model.Nome}");
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = verificamodeldenovo
            };
        }
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
    public async Task<string> VerificarAdvogadoAsync(AdvogadoDTO model, bool? isUpdate)
    {
        var mensagensErros = "";
        var listaAdvogados = await GetAdvogadosComIdAsync();

        foreach (var advogado in listaAdvogados)
        {
            if (advogado.Id != model.Id)  // Adiciona esta verificação para ignorar o próprio registro
            {
                if (advogado.Cpf == model.Cpf)
                {
                    mensagensErros += $"Advogado com CPF {model.Cpf} já está cadastrado no sistema.\n";
                }
                if (advogado.Oab == model.Oab)
                {
                    mensagensErros += $"Advogado com OAB {model.Oab} já está cadastrado no sistema.\n";
                }
            }
        }

        return string.IsNullOrEmpty(mensagensErros) ? null : mensagensErros;
    }
}