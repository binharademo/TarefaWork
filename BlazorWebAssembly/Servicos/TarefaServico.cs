using BlazorWebAssembly.DTO;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWebAssembly.Servicos
{
    public class TarefaServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<TarefaDTO>> Lista()
        {
            try
            {
                var response = await _http.GetAsync("/Tarefa");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<TarefaDTO>>() ?? [];

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar lista de tarefas: {ex.Message}");
            }
            return [];
        }

        public async Task<TarefaDTO?> Busca(int Id)
        {
            try
            {
                var response = await _http.GetAsync($"/Tarefa/{Id}");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<TarefaDTO>();

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar tarefas: {ex.Message}");
            }
            return null;
        }

        public async Task<string> Salva(int id, TarefaDTO tarefa)
        {
            try
            {
                HttpResponseMessage response;
                if (id == 0)
                    response = await _http.PostAsJsonAsync("/Tarefa", tarefa);
                else
                    response = await _http.PutAsJsonAsync($"/Tarefa/{id}", tarefa);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AtualizaStatus(int id, int novoStatus)
        {
            try
            {
                var response = await _http.PutAsync($"/Tarefa/{id}/Status/{novoStatus}", null);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public TarefaDTO NaoEncotrado() => new TarefaDTO
        {
            Id = -1,
            Titulo = "Não Encontrado"
        };
    }
}
