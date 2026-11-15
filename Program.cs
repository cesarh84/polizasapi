using Microsoft.EntityFrameworkCore;
using polizas_api.Datos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//area de servicios

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

//area de middlewares

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
