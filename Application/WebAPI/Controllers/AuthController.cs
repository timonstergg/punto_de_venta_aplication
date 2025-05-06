using Microsoft.AspNetCore.Mvc;



using Venta.Application.DTOs;
using Venta.Application.Interfaces;
using Venta.Application.Services;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly ICrearCuentaService _crearCuentaService;
        

        public AuthController(IJWTService jwtService,ICrearCuentaService crearCuentaService)
        {
            _jwtService = jwtService;
            _crearCuentaService = crearCuentaService;     
        }

        [HttpPost("login")]
        public async Task <ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _crearCuentaService.GetByCorreo(loginDTO.Email);

            if (account == null)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectas" });
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, account.Password);

            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });
            }

            var token = _jwtService.GenerateJwtToken(account.Email, account.Role.ToString());

            return Ok(new { token, role = account.Role.ToString(), email = account.Email, userId = account.Id, });
        }
    }
}

