using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Domain.Entities
{
    public class Venta:BaseEntity
    {
        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
       
        public List<DetalleVenta> Detalles { get; set; } = new();
        [Required]
        public MetodoPago MetodoPago { get; set; }
        [Required]
        public EstadoVenta Estado { get; set; } = EstadoVenta.Pendiente;
    }

    public enum EstadoVenta
    {
        Pendiente,
        Pagado,
        Cancelado
    }

    public enum MetodoPago
    {
        Efectivo,
        Tarjeta,
        Transferencia
    }
}
