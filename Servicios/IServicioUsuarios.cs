using Microsoft.AspNetCore.Identity;

namespace polizas_api.Servicios
{
    public interface IServicioUsuarios
    {
        Task<IdentityUser?> ObtenerUsuario();
    }
}