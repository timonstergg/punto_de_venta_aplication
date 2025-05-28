using Venta.Shared.DTOs.DTOs;

namespace Venta.BlazorUI.Services.Interface
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ObtenerCliente();
        Task<ClienteDTO?> ObtenerClientePorId(int id);
        Task<bool> CrearCliente(ClienteDTO clienteDTO);
        Task<bool> ActualizarCliente(ClienteDTO clienteDTO);
        Task<bool> EliminarCliente(int id);
    }
}
