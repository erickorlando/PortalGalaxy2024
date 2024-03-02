using Microsoft.EntityFrameworkCore;
using PortalGalaxy.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PortalGalaxyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalGalaxy"));
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

app.MapGet("api/Categorias", async (PortalGalaxyDbContext context) =>
{
    var data = await context.Categoria
        .Select(p => new
        {
            p.Id,
            p.Nombre,
            p.Descripcion
        })
        .ToListAsync();

    return Results.Ok(data);
});

app.MapGet("api/Talleres", async (PortalGalaxyDbContext context) =>
{
    var data = await context.Talleres.ToListAsync();

    return Results.Ok(data);
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
