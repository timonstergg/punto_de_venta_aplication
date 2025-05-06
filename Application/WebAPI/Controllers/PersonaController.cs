using Microsoft.AspNetCore.Mvc;

using Venta.Application.Interfaces;
using Venta.Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/Personas")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                await _personaService.MostrarPersonas();
                return Ok(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener personas", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                await _personaService.ObtenerPersonaPorId(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener persona", error = ex.Message });
            }
        }

      
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Persona persona)
        {
            if (persona == null)
                return BadRequest(new { message = "El objeto persona no puede ser nulo" });

            try
            {
                await _personaService.AgregarPersona((persona.Documento), persona.Nombre); // usa documento como id
                return Ok(new { message = "Persona agregada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar persona", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _personaService.EliminarPersona(id);
                return Ok(new { message = "Persona eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar persona", error = ex.Message });
            }
        }
    }
}
