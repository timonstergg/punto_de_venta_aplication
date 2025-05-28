using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Interfaces;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;
using Venta.Infrastructure.Repository;

namespace Venta.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<Cliente> _repository;
        public ClienteService(IRepository<Cliente> repository)
        {
            _repository = repository;
        }

        public async Task<Cliente> ActualizarCliente(Cliente cliente)
        {
            var clienteExistente = await _repository.GetById(cliente.Id);

            if (clienteExistente == null)
                throw new Exception($"No se encontró un producto con ID {cliente.Id}");

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Documento = cliente.Documento;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Telefono = cliente.Telefono;


            await _repository.Update(clienteExistente);
            await _repository.Save();

            return clienteExistente;
        }

        public async Task AgregarCliente(Cliente cliente)
        {
            await _repository.Add(cliente);
            await _repository.Save();

        }

        public async  Task<bool> EliminarCliente(int id)
        {
            try
            {
                var cliente = await _repository.GetById(id);

                if (cliente == null)
                {
                    return false;
                }

                await _repository.DeleteById(cliente.Id);
                await _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Cliente>> MostrarClientes()
        {
            var clientes = await _repository.GetAll();

            Console.WriteLine("Lista de clientes:");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Documento: {cliente.Telefono}, Nombre: {cliente.Direccion}");
            }

            return clientes;
        }

        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            try
            {
                var cliente = await _repository.GetById(id);

                if (cliente != null)
                {
                    Console.WriteLine($"cliente encontrado: {cliente.Nombre}");
                }
                else
                {
                    Console.WriteLine("cliente no encontrado.");
                }

                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
