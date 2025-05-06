﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Domain.Entities
{
    public class DetalleVenta:BaseEntity
    {
        [Required]
        public int VentaId { get; set; }
        [ForeignKey("VentaId")]
        public Venta? Venta { get; set; }
        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
