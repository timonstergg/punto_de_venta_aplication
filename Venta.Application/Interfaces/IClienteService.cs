using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Entities;

namespace Venta.Application.Interfaces
{
    public interface IClienteService
    {
        Task AgregarCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> MostrarClientes();
        Task<Cliente?> ObtenerClientePorId(int id);
        Task<bool> EliminarCliente(int id);
        Task<Cliente> ActualizarCliente(Cliente cliente);
    }
}
