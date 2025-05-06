using Microsoft.AspNetCore.Mvc;

using Venta.Application.DTOs;
using Venta.Application.Interfaces;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearCuentaController : Controller
    {
        private readonly ICrearCuentaService _crearCuentaService;
        private readonly IRepository<CrearCuenta> _repository;


        public CrearCuentaController(ICrearCuentaService crearCuentaService, IRepository<CrearCuenta> repository)
        {
            _crearCuentaService = crearCuentaService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() {

            var obj = await _repository.GetAll();

            if (obj == null || !obj.Any())
            {
                return NotFound("No se encontraron cuentas.");
            }

            return  Ok(obj);
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var obj = await _crearCuentaService.GetDataNameId();

                if (obj == null || !obj.Any())
                {
                    return NotFound("No se encontraron cuentas.");
                }

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task <ActionResult> GetByIdAccount(int id)
        {
            try
            {
                var obj = await _repository.GetById(id);

                if (obj == null)
                {
                    NotFound(new { message = $"No se encontró un usuario con ID {id}." });
                }
                return Ok(new
                {
                    obj.Id,
                    obj.Name,
                    obj.Email,
                    Role = obj.Role.ToString()
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno en el servidor.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> crearCuentaPost([FromBody] CrearCuentaDTO crearCuenta)
        {
            if (crearCuenta == null)
            {
                return BadRequest(new { message = "El objeto no puede ser nulo" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAccount = await _crearCuentaService.GetByCorreo(crearCuenta.Email);
            if (existingAccount != null)
            {
                return BadRequest(new { message = "El correo ya existe en el sistema." });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(crearCuenta.Password);

            var nuevauenta = new CrearCuenta
            {
                Name = crearCuenta.Name,
                Email = crearCuenta.Email,
                Password = hashedPassword, 
                Role = UserRole.User
            };

            _repository.Add(nuevauenta);

            return CreatedAtAction(nameof(crearCuentaPost), new { id = nuevauenta.Id }, new
            {
                nuevauenta.Id,
                nuevauenta.Name,
                nuevauenta.Email,
                nuevauenta.Role
            });
        }
    }
}
