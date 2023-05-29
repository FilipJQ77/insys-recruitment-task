using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(int id)
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
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}