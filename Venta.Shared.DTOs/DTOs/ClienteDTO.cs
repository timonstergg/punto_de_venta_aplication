using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Shared.DTOs.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
