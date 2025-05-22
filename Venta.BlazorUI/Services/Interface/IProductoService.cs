using Venta.Shared.DTOs.DTOs;

namespace Venta.BlazorUI.Services.Interface
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> ObtenerProductos();
        Task<ProductoDTO?> ObtenerProductoPorId(int id);
        Task<bool> CrearProducto(ProductoDTO producto);
        Task<bool> ActualizarProducto(ProductoDTO producto);
        Task<bool> EliminarProducto(int id);
    }
}
