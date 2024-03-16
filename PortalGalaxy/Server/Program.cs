using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Profiles;
using PortalGalaxy.Shared.Configuracion;
using Scrutor;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuracion de Logs
var logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("PortalGalaxy"),
        new MSSqlServerSinkOptions
        {
            AutoCreateSqlDatabase = true,
            AutoCreateSqlTable = true,
            TableName = "GalaxyLogs"
        }, restrictedToMinimumLevel: LogEventLevel.Warning)
    .CreateLogger();

// Seteamos a Serilog como proveedor de Logs
builder.Logging.AddSerilog(logger);

if (builder.Environment.IsDevelopment())
{
    var loggerFile = new LoggerConfiguration()
        .WriteTo.Console(LogEventLevel.Information)
        .WriteTo.File("..\\registro.log",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] - {Message}{NewLine}{Exception}",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Warning)
        .CreateLogger();

    builder.Logging.AddSerilog(loggerFile);
}

// Mapeamos el contenido del archivo de configuracion en una clase fuertemente tipada
builder.Services.Configure<AppSettings>(builder.Configuration);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PortalGalaxyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalGalaxy"));
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GalaxySecurity"));
});

// Configuramos ASP.NET Identity Core
builder.Services.AddIdentity<GalaxyIdentityUser, IdentityRole>(policies =>
    {
        policies.Password.RequireDigit = false;
        policies.Password.RequireLowercase = true;
        policies.Password.RequireUppercase = true;
        policies.Password.RequireNonAlphanumeric = false;
        policies.Password.RequiredLength = 8;

        policies.User.RequireUniqueEmail = true;

        // Politica de bloqueo de cuentas
        policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        policies.Lockout.MaxFailedAccessAttempts = 3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
    o.TokenLifespan = TimeSpan.FromHours(1));

// Registramos las dependencias de Repositories
builder.Services.Scan(selector => selector
    .FromAssemblies(typeof(ICategoriaRepository).Assembly,
        typeof(IUserService).Assembly)
    .AddClasses(publicOnly: false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime()
);

builder.Services.AddAutoMapper(conf =>
{
    conf.AddProfile<CategoriaProfile>();
    conf.AddProfile<TallerProfile>();
    conf.AddProfile<InstructorProfile>();
    conf.AddProfile<AlumnoProfile>();
    conf.AddProfile<InscripcionProfile>();
});

// Configuramos el contexto de seguridad del API
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                           throw new InvalidOperationException("No se configuro el SecretKey"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Autenticacion
app.UseAuthentication();
// Autorizacion
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();
