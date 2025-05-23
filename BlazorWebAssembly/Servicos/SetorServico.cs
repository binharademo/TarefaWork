﻿using BlazorWebAssembly.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Servicos
{
    public class SetorServico(HttpClient httpClient)
    {
        private readonly HttpClient _http = httpClient;

        public async Task<List<SetorDTO>> Lista()
        {
            try
            {
                var response = await _http.GetAsync("/Setor");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<SetorDTO>>() ?? [];

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar lista de setores: {ex.Message}");
            }
            return [];
        }

        public async Task<SetorDTO?> Busca(int Id)
        {
            try
            {
                var response = await _http.GetAsync($"/Setor/{Id}");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<SetorDTO>();

                Console.WriteLine($"API retornou status {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar setor: {ex.Message}");
            }
            return null;
        }

        public async Task<string> Salva(int id, SetorDTO setor)
        {
            try
            {
                HttpResponseMessage response;
                if (id == 0)
                    response = await _http.PostAsJsonAsync("/Setor", setor);
                else
                    response = await _http.PutAsJsonAsync($"/Setor/{id}", setor);

                if (response.IsSuccessStatusCode)
                    return string.Empty;

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public SetorDTO NaoEncotrado() => new SetorDTO
        {
            Id = -1,
            Nome = "Não Encontrado"
        };
    }
}
