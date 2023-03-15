using System;
using System.Linq.Expressions;

namespace Iterates.Bwm.Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(T entity);
}

