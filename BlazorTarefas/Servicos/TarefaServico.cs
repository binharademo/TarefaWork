using ApiRest.DTOs;
using System.Net.Http;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class TarefaServico
    {
        private readonly HttpClient _httpClient;
        //private readonly UsuarioServico _usuario;
        //private int _nextId = 1;

        public TarefaServico(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        //public TarefaServico(UsuarioServico usuario)
        //{
        //    _usuario = usuario;
        //}

        public async Task <TarefaDTO> Adicionar(CriarTarefaDTO tarefa)
        {
            var response = await _httpClient.PostAsJsonAsync("tarefa", tarefa);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TarefaDTO>();
        }

        public async Task <bool> Atualizar(int id, TarefaDTO tarefa)
        {
            var response = await _httpClient.PutAsJsonAsync($"tarefa/{id}", tarefa);
            return response.IsSuccessStatusCode;
        }


        public async Task<List<TarefaDTO>> BuscaTodos()
        {
            return await _httpClient.GetFromJsonAsync<List<TarefaDTO>>("tarefa")
                   ?? new List<TarefaDTO>();
        }

        public async Task<TarefaDTO?> BuscaPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<TarefaDTO?>($"tarefa/{id}");
        }
        //public Task Remover(int id)
        //{
        //    var tarefa = _tarefa.FirstOrDefault(t => t.Id == id);
        //    if (tarefa != null) _tarefa.Remove(tarefa);
        //    return Task.CompletedTask;
        //}

    }
}
