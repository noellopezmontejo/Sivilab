using Sivilab.Web.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/sin-acceso";
        // Al estar en HTTP puro (localhost), forzamos a que no requiera etiqueta "Secure"
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Name = ".Sivilab.AuthCookieHTTPLocal";
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.Name = ".Sivilab.AntiforgeryHTTPLocal";
});

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("SivilabApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7264/");
});

// Registrar para inyectar HttpClient directamente si es necesario, aunque lo ideal es usar IHttpClientFactory
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SivilabApi"));

builder.Services.AddScoped<Sivilab.Web.Services.ICandidatoCrpService, Sivilab.Web.Services.CandidatoCrpService>();
builder.Services.AddScoped<Sivilab.Web.Services.IOfertaService, Sivilab.Web.Services.OfertaService>();
builder.Services.AddScoped<Sivilab.Web.Services.IEmpresaService, Sivilab.Web.Services.EmpresaService>();
builder.Services.AddScoped<Sivilab.Web.Services.IAccesoWebService, Sivilab.Web.Services.AccesoWebService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Login endpoints
app.MapPost("/api/auth/login", async (Microsoft.AspNetCore.Http.HttpContext ctx, [Microsoft.AspNetCore.Mvc.FromForm] string email, [Microsoft.AspNetCore.Mvc.FromForm] string password, Sivilab.Web.Services.IAccesoWebService _acceso) =>
{
    // Cifrado y validación contra API
    var valido = await _acceso.ValidarCredenciales(email, password);
    if (!valido) return Microsoft.AspNetCore.Http.Results.Redirect("/login?error=true");

    var usuario = await _acceso.ObtenerPorEmail(email);
    if (usuario == null) return Microsoft.AspNetCore.Http.Results.Redirect("/login?error=true");

    var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.UserName ?? string.Empty),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuario.Email ?? string.Empty),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, usuario.Role ?? "Candidato")
    };

    var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(ctx, CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

    return Microsoft.AspNetCore.Http.Results.Redirect("/accesos");
}).DisableAntiforgery();

app.MapPost("/api/auth/logout", async (Microsoft.AspNetCore.Http.HttpContext ctx) =>
{
    await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(ctx, CookieAuthenticationDefaults.AuthenticationScheme);
    return Microsoft.AspNetCore.Http.Results.Redirect("/");
});

app.Run();
