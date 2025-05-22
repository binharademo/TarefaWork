using BlazorWebAssembly.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Servicos
{
    public class EmpresaServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<EmpresaDTO>> Lista()
        {
            try
            {
                var response = await _http.GetAsync("Empresa");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<EmpresaDTO>>() ?? [];

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar lista de empresas: {ex.Message}");
            }
            return [];
        }

        public async Task<EmpresaDTO?> Busca(int Id)
        {
            try
            {
                var response = await _http.GetAsync($"Empresa/{Id}");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<EmpresaDTO>();

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar empresa: {ex.Message}");
            }
            return null;
        }
    }
}
