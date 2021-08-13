using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaroTest.Infrastructure.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ClaroTestDbContext _dbContext;
        public GenericRepository(ClaroTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<List<T>> GetAllAsync() => 
            await _dbContext.Set<T>().ToListAsync();

        public async Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> expression) =>
            await _dbContext.Set<T>().Where(expression).ToListAsync();

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression) =>
            await _dbContext.Set<T>().Where(expression).FirstOrDefaultAsync();

        public void Remove(T entity) => 
            _dbContext.Set<T>().Remove(entity);

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public async Task<bool> SaveChangesAsync() => 
            (await _dbContext.SaveChangesAsync() > 0) ? true : false;
    }
}
