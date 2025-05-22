using Microsoft.AspNetCore.Mvc;

using Venta.Application.DTOs;
using Venta.Application.Interfaces;
using Venta.Application.Services;
using Venta.Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductos()
        {

            var obj = await _productoService.MostrarProductos();

            if (obj == null || !obj.Any())
            {
                return NotFound("No se encontraron productos.");
            }

            return Ok(obj);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdProduct(int id)
        {
            try
            {
                var obj = await _productoService.ObtenerProductoPorId(id);

                if (obj == null)
                {
                    NotFound(new { message = $"No se encontró el producto con ID {id}." });
                }
                return Ok(new
                {

                    obj.Nombre,
                    obj.Precio,
                    obj.Stock
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno en el servidor.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto([FromBody] ProductoDTO productoDto)
        {
            if (productoDto == null)
                return BadRequest(new { message = "Los datos del producto son obligatorios." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoProducto = new Producto
            {
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };

            await _productoService.AgregarProducto(nuevoProducto);

            return CreatedAtAction(nameof(GetByIdProduct), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _productoService.EliminarProducto(id);

                if (!eliminado)
                    return NotFound(new { message = $"No se encontró el producto con ID {id}" });

                return Ok(new { message = "Producto eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el producto", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProducto(int id, [FromBody] ProductoDTO productoDto)
        {
            if (productoDto == null || id == 0)
                return BadRequest(new { message = "Datos inválidos para la actualización." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producto = new Producto
            {
                Id = id,
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };

            try
            {
                var actualizado = await _productoService.ActualizarProducto(producto);

                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
