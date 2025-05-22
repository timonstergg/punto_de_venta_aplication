using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Domain.Entities;

namespace Venta.Application.Interfaces
{
    public interface IProductoService
    {
        Task AgregarProducto(Producto producto);
        Task<IEnumerable<Producto>> MostrarProductos();
        Task<Producto?> ObtenerProductoPorId(int id);
        Task<bool> EliminarProducto(int id);
        Task<Producto> ActualizarProducto(Producto producto);
    }
}
