using ApiRest.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class SetorServico
    {
        private readonly HttpClient _httpClient;

        public SetorServico(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<SetorDTO> Adicionar(CriarSetorDTO setor)
        {
            var response = await _httpClient.PostAsJsonAsync("setor", setor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SetorDTO>();
        }

        public async Task<bool> Atualizar(int id, AlterarSetorDTO setor)
        {
            // DEBUG: Verifique o que está sendo enviado
            Console.WriteLine($"HTTP Client enviando - Status: {setor.Status}");

            var response = await _httpClient.PutAsJsonAsync($"setor/{id}", setor);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SetorDTO>> BuscaTodos()
        {
            return await _httpClient.GetFromJsonAsync<List<SetorDTO>>("setor")
                   ?? new List<SetorDTO>();
        }

        public async Task<SetorDTO?> BuscaPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<SetorDTO?>($"setor/{id}");
        }

        public async Task<bool> Remover(int id)
        {
            var response = await _httpClient.DeleteAsync($"setor/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}