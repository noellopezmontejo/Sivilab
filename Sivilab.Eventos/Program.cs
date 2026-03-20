using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Sivilab.Eventos.Components;
using Sivilab.Eventos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar HttpClient para la API
builder.Services.AddHttpClient("SivilabAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7264/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// REGISTRAR SERVICIOS DEL PROYECTO EVENTOS
builder.Services.AddScoped<ICandidatoCrpService, CandidatoCrpService>();
builder.Services.AddScoped<IAccesoWebService, AccesoWebService>();
builder.Services.AddScoped<IVacanteService, VacanteService>();

// NOTA: IVacanteRepository y VacanteRepository NO deben registrarse aquí
// porque este proyecto es un frontend que consume la API.
// Los repositorios van en el proyecto Sivilab.API.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
