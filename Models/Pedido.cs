using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaToxicaCRUD.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(100)]
        public string Cliente { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public decimal Total { get; set; }

        // Un pedido tiene muchos detalles (platos pedidos)
        public List<DetallePedido> Detalles { get; set; }
    }
}
