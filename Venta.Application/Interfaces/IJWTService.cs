using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Application.Interfaces
{
    public interface IJWTService
    {
       Task <string> GenerateJwtToken(string username, string role);
    }
}
