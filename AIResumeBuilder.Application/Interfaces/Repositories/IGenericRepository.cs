using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        void Delete(T Entity);
        void Update(T Entity);

    }
}
