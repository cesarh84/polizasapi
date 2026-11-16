using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using polizas_api.DTOs;
using polizas_api.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace polizas_api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuariosController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IServicioUsuarios servicioUsuarios;

        public UsuariosController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, IServicioUsuarios servicioUsuarios)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(CredencialesUsuarioDTO credencialesUsuarioDTO) 
        {
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuarioDTO.Email,
                Email = credencialesUsuarioDTO.Email
            };

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDTO.Password);    

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);
                return respuestaAutenticacion;
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return ValidationProblem();
            }

        }

        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);

            if (usuario == null)
            {
                return RetornarLoginIncorrecto();
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesUsuarioDTO.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuarioDTO);
            }
            else
            {
                return RetornarLoginIncorrecto();
            }
        }

        private ActionResult RetornarLoginIncorrecto()
        {
            ModelState.AddModelError(string.Empty, "Login incorrecto");
            return ValidationProblem();
        }

        [HttpGet("renovarToken")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Renovar()
        {
            var usuario = await servicioUsuarios.ObtenerUsuario();

            if (usuario == null)
            {
                return NotFound();
            }
            
            var credencialesUsuarioDTO = new CredencialesUsuarioDTO
            {
                Email = usuario.Email!,
                Password = ""
            };

            var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);
            return respuestaAutenticacion;

        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuarioDTO.Email),
                new Claim("loquesea", "cualquiercosa")
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

            return new RespuestaAutenticacionDTO()
            {
                Token = token,
                Expiracion = expiracion
            };
        }
    }
}
