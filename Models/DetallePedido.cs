using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaToxicaCRUD.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetalle { get; set; }

        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        public int PlatoId { get; set; }
        [ForeignKey("PlatoId")]
        public Plato Plato { get; set; }

        [Range(1, 1000, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Cantidad { get; set; }

        public decimal Subtotal { get; set; }
    }
}
