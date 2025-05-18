using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }
}