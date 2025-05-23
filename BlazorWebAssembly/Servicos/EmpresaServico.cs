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


        public async Task<string> Salva(int id, EmpresaDTO empresa)
        {
            try
            {
                HttpResponseMessage response;
                if (id == 0)
                    response = await _http.PostAsJsonAsync("/Empresa", empresa);
                else
                    response = await _http.PutAsJsonAsync($"/Empresa/{id}", empresa);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public EmpresaDTO NaoEncotrado() => new EmpresaDTO
        {
            Id = -1,
            Nome = "Não Encontrado"
        };
    }
}
