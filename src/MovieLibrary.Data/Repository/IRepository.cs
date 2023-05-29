﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibrary.Data.Repository;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetFilteredAsync(Func<TEntity, bool> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}