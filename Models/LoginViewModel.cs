using System.ComponentModel.DataAnnotations;

namespace LaToxicaCRUD.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresa tu usuario")]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingresa tu contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }
    }
}
