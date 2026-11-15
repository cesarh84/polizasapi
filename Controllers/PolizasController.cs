using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polizas_api.Datos;
using polizas_api.Entidades;

namespace polizas_api.Controllers
{
    [ApiController]
    [Route("api/polizas")]
    public class PolizasController: ControllerBase  
    {
        private readonly ApplicationDbContext context;

        public PolizasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Poliza>> Get()
        {
            return await context.Polizas.ToListAsync();
        }

        [HttpGet("{id:int}")] //api/polizas/id
        public async Task<ActionResult<Poliza>> Get(int id)
        {
            var poliza = await context.Polizas
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (poliza == null)
            {
                return NotFound();
            }
            return poliza;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Poliza poliza)
        {
            var existeCliente = await context.Clientes.AnyAsync(x => x.Id == poliza.ClienteId);
            if (!existeCliente)
            {
                return BadRequest($"El cliente de la poliza id {poliza.Id} no existe");
            }

            context.Add(poliza);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/polizas/id
        public async Task<ActionResult> Put(Poliza poliza, int id)
        {
            if (poliza.Id != id)
            {
                return BadRequest("Los ids deben de coincidir.");
            }

            var existeCliente = await context.Clientes.AnyAsync(x => x.Id == poliza.ClienteId);
            if (!existeCliente)
            {
                return BadRequest($"El cliente de la poliza id {poliza.Id} no existe");
            }

            context.Update(poliza);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] //api/polizas/id
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Polizas.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
