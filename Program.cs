using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using polizas_api.Datos;
using polizas_api.Servicios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//area de servicios

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

//area de middlewares

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
