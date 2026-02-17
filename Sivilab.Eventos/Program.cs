using Sivilab.Eventos.Components;
using Sivilab.Eventos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar HttpClient para consumir la API
builder.Services.AddHttpClient("SivilabAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7264");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Registrar SOLO los servicios que consumen la API
builder.Services.AddScoped<ICandidatoCrpService, CandidatoCrpService>();
builder.Services.AddScoped<IAccesoWebService, AccesoWebService>(); // AGREGAR ESTA LÍNEA


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
