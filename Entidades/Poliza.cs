using System.ComponentModel.DataAnnotations;

namespace polizas_api.Entidades
{
    public class Poliza
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(100, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        public required string PolizaNombre { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
