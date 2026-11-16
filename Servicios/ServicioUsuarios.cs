using Microsoft.AspNetCore.Identity;

namespace polizas_api.Servicios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public ServicioUsuarios(UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }

        public async Task<IdentityUser?> ObtenerUsuario()
        {
            var emailClaim = contextAccessor.HttpContext!.User.Claims.Where(c => c.Type == "email").FirstOrDefault();

            if (emailClaim == null)
            {
                return null;
            }
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            return usuario;
        }
    }
}
