using Microsoft.EntityFrameworkCore;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;

namespace PortalGalaxy.Repositories.Implementaciones;

public class AlumnoRepository : RepositoryBase<Alumno>, IAlumnoRepository
{
    public AlumnoRepository(PortalGalaxyDbContext context) : base(context)
    {
    }

    public async Task<Alumno?> FindByEmailAsync(string email)
    {
        return await Context.Set<Alumno>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Correo == email);
    }
}