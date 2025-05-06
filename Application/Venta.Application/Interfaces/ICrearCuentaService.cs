using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Application.DTOs;
using Venta.Domain.Entities;

namespace Venta.Application.Interfaces
{
    public interface ICrearCuentaService
    {
       Task<CrearCuenta> GetByCorreo(string correo);

      Task <IEnumerable<UserDTO>> GetDataNameId();
   }
}
