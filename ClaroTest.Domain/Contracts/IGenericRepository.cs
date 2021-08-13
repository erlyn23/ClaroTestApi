using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> expression);
        Task<T> GetOneAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Remove(T entity);
        Task<bool> SaveChangesAsync();
    }
}
