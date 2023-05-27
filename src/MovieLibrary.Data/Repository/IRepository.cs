using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibrary.Data.Repository;

public interface IRepository<TEntity>
{
    Task<TEntity> GetAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}