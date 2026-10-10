using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LaToxicaCRUD.Models
{
    public class Plato
    {
        [Key]
        public int IdPlato { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede pasar de 200 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [StringLength(300, ErrorMessage = "La URL no puede pasar de 300 caracteres")]
        [RegularExpression(@"^(https?://|/).*", ErrorMessage = "La imagen debe empezar con http://, https:// o /")]
        [Display(Name = "Imagen (URL)")]
        public string ImagenUrl { get; set; }

        [Range(0.01, 999, ErrorMessage = "Ingrese un precio válido (mayor a 0 y menor a 999)")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Precio { get; set; }

        [Range(0, 10000, ErrorMessage = "El stock debe estar entre 0 y 10000")]
        public int Stock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        [ValidateNever]
        public Categoria Categoria { get; set; }
    }
}
