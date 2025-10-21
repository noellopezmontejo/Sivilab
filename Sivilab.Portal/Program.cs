using Sivilab.Portal.Services;
using Sivilab.Portal.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ⭐ Registrar el servicio de eventos
builder.Services.AddScoped<EventoService>();

// ⭐ NUEVO: Registrar el filtro de autenticación
builder.Services.AddScoped<AdminAuthFilter>();

// ⭐ NUEVO: Configurar Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // La sesión expira en 30 minutos
    options.Cookie.HttpOnly = true; // Mayor seguridad
    options.Cookie.IsEssential = true; // Necesario para GDPR
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ⭐ NUEVO: Habilitar Sessions (debe ir ANTES de UseAuthorization)
app.UseSession();

app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();