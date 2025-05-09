using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.BusinessLogic.Services;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsuarioEmpresaService, UsuarioEmpresaService>();
builder.Services.AddScoped<IUsuarioInfluencerService, UsuarioInfluencerService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICiudadService, CiudadService>();
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IOfertaServicioService, OfertaServicioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IRedSocialService, RedSocialService>();


//Add repositories to the container.
builder.Services.AddScoped<IUsuarioEmpresaRepository, UsuarioEmpresaRepository>();
builder.Services.AddScoped<IUsuarioInfluencerRepository, UsuarioInfluencerRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICiudadRepository, CiudadRepository>();
builder.Services.AddScoped<IPaisRepository, PaisRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IOfertaServicioRepository, OfertaServicioRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IRedSocialRepository, RedSocialRepository>();

//Add Helpers to the container.
NotificacionesCorreoHelper.Configurar(builder.Configuration);


// Add authentication
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Sesion/InicioSesion";  // Redirige si no está autenticado
        options.AccessDeniedPath = "/Sesion/AccesoDenegado";  // Redirige si no tiene permisos
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Expira en 1 hora
    });

// Add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Empresa", policy => policy.RequireRole("Empresa"));
    options.AddPolicy("Influencer", policy => policy.RequireRole("Influencer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");

app.Run();
