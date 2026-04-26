using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetLastAsync();
        Task AddAsync(T entity);
        Task SaveChangesAsync();
    }
}
