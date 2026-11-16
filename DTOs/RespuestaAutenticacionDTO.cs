namespace polizas_api.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
    }
}
