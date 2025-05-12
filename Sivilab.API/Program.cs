using Microsoft.Data.SqlClient;
using Sivilab.API.Middleware;
using Sivilab.API.Services;
using Sivilab.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Registro de servicios del repositorio
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();

// Agregar la conexión a la base de datos
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddOpenApi();

// Configuración para JWT
builder.Services.AddSingleton<IJwtService>(new JwtService(builder.Configuration["Jwt:SecretKey"]));


var app = builder.Build();

// Use the JWT middleware
app.UseMiddleware<JwtMiddleware>(builder.Configuration["Jwt:SecretKey"]);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
