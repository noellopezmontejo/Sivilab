
using Sivilab.Data.Repositories;
using Sivilab.Eventos.Components;
using Sivilab.Eventos.Components;
using Sivilab.Eventos.Services;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Registrar el repositorio de candidatos
builder.Services.AddScoped<ICandidatoCrpService, CandidatoCrpService>();
// Registrar repositorio y servicio de Acceso_Web
builder.Services.AddScoped<IAccesoWebRepository, AccesoWebRepository>();
builder.Services.AddScoped<IAccesoWebService, AccesoWebService>();

// Configurar HttpClient para consumir la API
builder.Services.AddHttpClient("SivilabAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7264");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped(sp => 
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("SivilabAPI"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
