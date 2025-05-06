using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Domain.Entities;

namespace Venta.Application.Interfaces
{
    public interface IPersonaService
    {
        Task AgregarPersona(string documento, string nombre);
        Task<IEnumerable<Persona>> MostrarPersonas();
        Task ObtenerPersonaPorId(int id);
        Task EliminarPersona(int id);
    }
}
