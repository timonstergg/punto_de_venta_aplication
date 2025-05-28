using Microsoft.AspNetCore.Mvc;
using Venta.Application.DTOs;
using Venta.Application.Interfaces;
using Venta.Domain.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService) {

            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult> GetClientes()
        {
            var obj = await _clienteService.MostrarClientes();
            if (obj == null || !obj.Any())
                return NotFound("No se encontraron clientes.");

            return Ok(obj);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdCliente(int id)
        {
            try
            {
                var obj = await _clienteService.ObtenerClientePorId(id);
                if (obj == null)
                    return NotFound(new { message = $"No se encontró el cliente con ID {id}." });

                return Ok(new
                {
                    obj.Id,
                    obj.Nombre,
                    obj.Documento,
                    obj.Telefono,
                    obj.Direccion
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
                return BadRequest(new { message = "Los datos del cliente son obligatorios." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoCliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Documento = clienteDTO.Documento,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion
            };

            await _clienteService.AgregarCliente(nuevoCliente);
            return CreatedAtAction(nameof(GetByIdCliente), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _clienteService.EliminarCliente(id);
                if (!eliminado)
                    return NotFound(new { message = $"No se encontró el cliente con ID {id}" });

                return Ok(new { message = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el cliente", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {


            if (clienteDTO == null || id == 0)
                return BadRequest(new { message = "Datos inválidos para la actualización." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var clientes = new Cliente
            {
                Id = id,
                Nombre = clienteDTO.Nombre,
                Documento = clienteDTO.Documento,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion
            };

            try
            {
                var ActualizarCliente = await _clienteService.ActualizarCliente(clientes);

                return Ok(ActualizarCliente);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
