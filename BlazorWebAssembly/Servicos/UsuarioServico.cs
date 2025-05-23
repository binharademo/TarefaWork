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
                var response = await _http.GetAsync("/Usuario");

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
                var response = await _http.GetAsync($"/Usuario/{Id}");

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

        public async Task<string> Salva(int id, UsuarioDTO usuario)
        {
            try
            {
                HttpResponseMessage response;
                if (id == 0)
                    response = await _http.PostAsJsonAsync("/Usuario", usuario);
                else
                    response = await _http.PutAsJsonAsync($"/Usuario/{id}", usuario);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public UsuarioDTO NaoEncotrado() => new UsuarioDTO
        {
            Id = -1,
            Nome = "Não Encontrado"
        };
    }
}
