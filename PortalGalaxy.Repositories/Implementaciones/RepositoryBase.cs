using Microsoft.EntityFrameworkCore;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalGalaxy.Repositories.Implementaciones;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly DbContext Context;

    protected RepositoryBase(DbContext context)
    {
        Context = context;
    }

    public async Task<ICollection<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>()
            .Where(p => p.Estado)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>()
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicado,
        Expression<Func<TEntity, TInfo>> selector,
        string? relaciones = null)
    {
        var collection = Context.Set<TEntity>()
            .Where(predicado)
            .AsQueryable();

        // SELECT DE TALLERES "Instructor,Categoria"
        if (!string.IsNullOrWhiteSpace(relaciones))
        {
            foreach (var tabla in relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                collection = collection.Include(tabla);
            }
        }

        return await collection
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
    }

    public async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
        Expression<Func<TEntity, bool>> predicado,
        Expression<Func<TEntity, TInfo>> selector,
        Expression<Func<TEntity, TKey>> orderBy,
        string? relaciones = null,
        int pagina = 1, int filas = 5)
    {
        var query = Context.Set<TEntity>()
                   .Where(predicado)
                   .AsQueryable();

        // SELECT DE TALLERES "Instructor,Categoria"
        if (!string.IsNullOrWhiteSpace(relaciones))
        {
            foreach (var tabla in relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(tabla);
            }
        }

        var collection = await query
            .OrderBy(orderBy) // Esto ordena por una determinada columna
            .Skip((pagina -1) * filas) // Cantidad de registros a saltear
            .Take(filas) // Cantidad de filas a tomar
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();

        var total = await Context.Set<TEntity>()
            .Where(predicado)
            .CountAsync();

        return (collection, total);
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var registro = await FindAsync(id);
        if (registro is not null)
        {
            registro.Estado = false;
            await UpdateAsync();
        }
    }
}