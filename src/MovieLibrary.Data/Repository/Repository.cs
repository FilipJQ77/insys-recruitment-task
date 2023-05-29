using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public Task<List<TEntity>> GetFilteredAsync(Func<TEntity, bool> predicate)
    {
        return Task.FromResult(DbSet.Where(predicate).ToList());
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
}