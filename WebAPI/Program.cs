

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;

using Venta.Application.Interfaces;
using Venta.Application.Services;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;
using Venta.Infrastructure.Context;
using Venta.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IRepository<Persona>, Repository<Persona>>();
//builder.Services.AddScoped<IPersonaService, PersonaService>();
//builder.Services.AddScoped<ICrearCuentaService, CrearCuentaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.WithOrigins(
                "http://localhost:3000", // Permite solicitudes desde React
                "https://localhost:5003", // Blazor WASM
                "http://localhost:5003",  // Blazor WASM sin HTTPS
                "https://localhost:5173", // Vite
                "http://localhost:5173",
                "https://localhost:7058")
                        .AllowAnyMethod() // Permitir cualquier método (GET, POST, PUT, DELETE)
                        .AllowAnyHeader() // Permitir cualquier encabezado (Authorization, Content-Type, etc.)
                        .AllowCredentials()); // Permitir credenciales (tokens, cookies, etc.)
});

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),           
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection"));
});


builder.Services.AddScoped<IJWTService, JwtService>();
builder.Services.AddScoped<ICrearCuentaService, CrearCuentaService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
