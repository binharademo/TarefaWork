using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiRest.DTOs;

namespace BlazorTarefas.Servicos
{
    public class UsuarioServico
    {
        private readonly HttpClient _httpClient;

        public UsuarioServico(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<UsuarioDTO>> BuscaTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UsuarioDTO>>("usuario")
                   ?? new List<UsuarioDTO>();
        }

        public async Task<UsuarioDTO?> BuscaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioDTO?>($"usuario/{id}");
        }

        public async Task<UsuarioDTO?> AdicionarAsync(CriarUsuarioDTO usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("usuario", usuario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarUsuarioDTO usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"usuario/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

    }
}
