using ApiRest.DTOs;
using System.Net.Http;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class TarefaServico
    {
        private readonly HttpClient _httpClient;
        private List<TarefaDTO> _tarefa = new List<TarefaDTO>();
        //private readonly UsuarioServico _usuario;
        //private int _nextId = 1;

        public TarefaServico(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }


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

        public async Task<List<TarefaDTO>> BuscaPorUsuario(int usuarioId)
        {
            try
            {
                // Busca todas as tarefas da API
                var todasTarefas = await BuscaTodos();

                // Filtra as tarefas onde o usuário é o responsável OU o criador
                return todasTarefas?
                    .Where(t =>
                        (t.ResponsavelId != null && t.ResponsavelId == usuarioId) ||
                        (t.CriadorId != null && t.CriadorId == usuarioId))
                    .ToList() ?? new List<TarefaDTO>();
            }
            catch (Exception ex)
            {
                // Log do erro (em produção, use um logger adequado)
                Console.WriteLine($"Erro ao buscar tarefas por usuário: {ex.Message}");
                return new List<TarefaDTO>();
            }
        }

        public Task<List<TarefaDTO>> BuscaPorStatus(Tarefa.Status status ) =>
         Task.FromResult(_tarefa.Where(t => t.Status == status).ToList());


    }
}
