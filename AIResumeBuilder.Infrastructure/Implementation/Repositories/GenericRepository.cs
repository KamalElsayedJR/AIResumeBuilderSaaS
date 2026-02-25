using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T Entity)
        => _dbContext.Set<T>().Remove(Entity);
        

        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T Entity)
        => _dbContext.Set<T>().Update(Entity);
        
    }
}
