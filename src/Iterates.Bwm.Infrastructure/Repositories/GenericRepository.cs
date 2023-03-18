using System;
using Iterates.Bwm.Infrastructure.DbContexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Iterates.Bwm.Domain.Interfaces.Repositories;

namespace Iterates.Bwm.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<int> RemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        var rowsAffected = await _dbContext.SaveChangesAsync();

        return rowsAffected;
    }
}

