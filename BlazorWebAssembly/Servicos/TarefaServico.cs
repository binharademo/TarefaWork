using BlazorWebAssembly.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Servicos
{
    public class TarefaServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<TarefaDTO>> Lista()
        {
            try
            {
                var response = await _http.GetAsync("Tarefa");

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
                var response = await _http.GetAsync($"Tarefa/{Id}");

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
    }
}
