using BlazorWebAssembly.DTO;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Servicos
{
    public class UsuarioServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var response = await _http.GetAsync("Usuario");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<UsuarioDTO>>() ?? [];

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar lista de usuários: {ex.Message}");
            }
            return [];
        }

        public async Task<UsuarioDTO> Busca(int Id)
        {
            try
            {
                var response = await _http.GetAsync($"Usuario/{Id}");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<UsuarioDTO>() ?? NaoEncotrado();

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário: {ex.Message}");
            }
            return NaoEncotrado();
        }

        private UsuarioDTO NaoEncotrado() => new UsuarioDTO
        {
            Id = -1,
            Nome = "Não Encontrado"
        };
    }
}
