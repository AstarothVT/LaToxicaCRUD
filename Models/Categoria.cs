using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaToxicaCRUD.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        // Una categoria tiene muchos platos
        public List<Plato> Platos { get; set; }
    }
}
