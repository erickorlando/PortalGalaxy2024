using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PortalGalaxy.Shared;

namespace PortalGalaxy.DataAccess;

public static class UserDataSeeder
{
    public static async Task Seed(IServiceProvider service)
    {
        // UserManager (Repositorio de Usuarios)
        var userManager = service.GetRequiredService<UserManager<GalaxyIdentityUser>>();
        // RoleManager (Repositorio de Roles)
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear los roles
        var adminRole = new IdentityRole(Constantes.RolAdministrador);
        var clienteRole = new IdentityRole(Constantes.RolAlumno);

        if (!await roleManager.RoleExistsAsync(Constantes.RolAdministrador))
        {
            await roleManager.CreateAsync(adminRole);
        }
        
        if (!await roleManager.RoleExistsAsync(Constantes.RolAlumno))
        {
            await roleManager.CreateAsync(clienteRole);
        }

        // Creamos el usuario Administrador
        var adminUser = new GalaxyIdentityUser()
        {
            NombreCompleto = "Administrador del Sistema",
            UserName = "admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "pa$$W0rD@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
        }
    }
}