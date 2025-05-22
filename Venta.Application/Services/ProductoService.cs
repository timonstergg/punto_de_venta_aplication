using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Application.Interfaces;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;

namespace Venta.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IRepository<Producto> _repository;

        public ProductoService(IRepository<Producto> repository)
        {

            _repository = repository;
        }

        public async Task<Producto> ActualizarProducto(Producto producto)
        {
            var productoExistente = await _repository.GetById(producto.Id);

            if (productoExistente == null)
                throw new Exception($"No se encontró un producto con ID {producto.Id}");

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;

            await _repository.Update(productoExistente);
            await _repository.Save();

            return productoExistente;
        }

        public async Task AgregarProducto(Producto producto)
        {
            await _repository.Add(producto);
            await _repository.Save();
        }

        public async Task<bool> EliminarProducto(int id)
        {
            try
            {
                var producto = await _repository.GetById(id);

                if (producto == null)
                {
                    return false;
                }

                await _repository.DeleteById(producto.Id);
                await _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Producto>> MostrarProductos()
        {
            var producto = await _repository.GetAll();

            return producto;
        }

        public async Task<Producto?> ObtenerProductoPorId(int id)
        {
            try
            {
                var producto = await _repository.GetById(id);

                if (producto != null)
                {
                    Console.WriteLine($"Producto encontrado: {producto.Nombre}");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }

                return producto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
