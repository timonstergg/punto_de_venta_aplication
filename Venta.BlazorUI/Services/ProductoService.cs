using System.Net.Http.Json;

using Venta.BlazorUI.Services.Interface;
using Venta.Shared.DTOs.DTOs;

namespace Venta.BlazorUI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoDTO>> ObtenerProductos()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/Productos") ?? new();
        }

        public async Task<ProductoDTO?> ObtenerProductoPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoDTO>($"api/Productos/{id}");
        }

        public async Task<bool> CrearProducto(ProductoDTO producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Productos", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProducto(ProductoDTO producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Productos/{producto.Id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Productos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
