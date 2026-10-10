using System.ComponentModel.DataAnnotations;

namespace LaToxicaCRUD.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede pasar de 200 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        // Una categoría tiene muchos platos (relación 1 a N)
        public ICollection<Plato> Platos { get; set; }
    }
}
