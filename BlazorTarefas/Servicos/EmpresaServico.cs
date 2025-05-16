using ApiRest.DTOs;
using System.Net.Http;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class EmpresaServico
    {
        private List<EmpresaDTO> _empresa = new List<EmpresaDTO>();
        private readonly SetorServico _setor;
        private readonly HttpClient _httpClient;
        private int _nextId = 1;

        public EmpresaServico(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<EmpresaDTO> Adicionar(CriarEmpresaDTO empresa)
        {
            var response = await _httpClient.PostAsJsonAsync("empresa", empresa);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmpresaDTO>();
        }

        public async Task<bool> Atualizar(int id, EmpresaDTO empresa)
        {
            var response = await _httpClient.PutAsJsonAsync($"empresa/{id}", empresa);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EmpresaDTO>> BuscaTodos()
        {
            return await _httpClient.GetFromJsonAsync<List<EmpresaDTO>>("empresa")
                   ?? new List<EmpresaDTO>();
        }


        public async Task<EmpresaDTO?> BuscaPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmpresaDTO?>($"empresa/{id}");
        }
    }
}

