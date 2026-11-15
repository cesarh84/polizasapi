using polizas_api.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace polizas_api.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(100, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        [StringLength(100, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public required string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "El {0} no es válido")]
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public List<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}
