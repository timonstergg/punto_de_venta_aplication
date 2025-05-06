using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Application.DTOs;
using Venta.Application.Interfaces;
using Venta.Domain.Entities;
using Venta.Infrastructure.Context;

namespace Venta.Application.Services
{
    public class CrearCuentaService : ICrearCuentaService
    {
        private readonly ApplicationDbContext _context;

        public CrearCuentaService(ApplicationDbContext context)
        {

            _context = context;

        }
        public async Task<CrearCuenta> GetByCorreo(string correo)
        {
           return await _context.CrearCuentas.FirstOrDefaultAsync(x => EF.Property<string>(x, "Email") == correo);
        }

        public async Task<IEnumerable<UserDTO>> GetDataNameId()
        {
            try
            {
                var data = await _context.CrearCuentas
                                   .Select(obj => new UserDTO { Id = obj.Id, Name = obj.Name })
                                   .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de la base de datos", ex);
            }
        }
    }
}
