using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using polizas_api.Datos;
using polizas_api.DTOs;
using polizas_api.Entidades;
using polizas_api.Servicios;

namespace polizas_api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IServicioUsuarios servicioUsuarios;

        public ClientesController(ApplicationDbContext context, IMapper mapper, IServicioUsuarios servicioUsuarios)
        {
            this.context = context;
            this.mapper = mapper;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await context.Clientes.ToListAsync();
        }

        [HttpGet]
        [Route("/clientesdto")]
        public async Task<IEnumerable<ClienteDTO>> GetClientesDTO()
        {
            return await context.Clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                NombreCompleto = mapper.Map<ClienteDTO>(c).NombreCompleto
            }).ToListAsync();
        }

        [HttpGet("{id:int}")] //api/clientes/id?incluirPolizas=true|false
        public async Task<ActionResult<Cliente>> Get([FromRoute] int id,[FromQuery] bool incluirPolizas)
        {
            var cliente = await context.Clientes
                .Include(x => x.Polizas)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cliente cliente)
        {
            context.Add(cliente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/clientes/id
        public async Task<ActionResult> Put(Cliente cliente, int id)
        {
            if (cliente.Id != id)
            {
                return BadRequest("El id del cliente no coincide.");
            }
            context.Update(cliente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] //api/clientes/id
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Clientes.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
