using BlazorWebAssembly.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Servicos
{
    public class ComentarioServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<ComentarioDTO>> Lista(int tarefaId)
        {
            try
            {
                var response = await _http.GetAsync($"/Tarefa/{tarefaId}/Comentario");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<ComentarioDTO>>() ?? [];

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar lista de comentários: {ex.Message}");
            }
            return [];
        }

        public async Task<string> Salva(int tarefaId, ComentarioDTO comentario)
        {
            try
            {
                HttpResponseMessage response;
                if (comentario.Id == 0)
                    response = await _http.PostAsJsonAsync($"/Tarefa/{tarefaId}/Comentario", comentario);
                else
                    response = await _http.PutAsJsonAsync($"/Tarefa/{tarefaId}/Comentario{comentario.Id}", comentario);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
