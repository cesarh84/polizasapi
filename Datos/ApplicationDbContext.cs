using Microsoft.EntityFrameworkCore;
using polizas_api.Entidades;

namespace polizas_api.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Poliza> Polizas { get; set; }

        protected ApplicationDbContext()
        {
        }
    }
}
