using System.Net.Http.Json;

using Venta.BlazorUI.Services.Interface;
using Venta.Shared.DTOs.DTOs;

namespace Venta.BlazorUI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<bool> ActualizarCliente(ClienteDTO clienteDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cliente/{clienteDTO.Id}", clienteDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CrearCliente(ClienteDTO clienteDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cliente", clienteDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Cliente/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ClienteDTO>> ObtenerCliente()
        {
            return await _httpClient.GetFromJsonAsync<List<ClienteDTO>>("api/Cliente") ?? new();
        }

        public Task<ClienteDTO?> ObtenerClientePorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
