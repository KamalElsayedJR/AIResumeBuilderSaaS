using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Hashtable _repo = new Hashtable();
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            ResumeRepository = new ResumeRepository(_dbContext);
            ExperienceRepository = new ExperienceRepository(_dbContext);
            SkillRepositroy = new SkillRepository(_dbContext);
        }
        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();
        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repo.ContainsKey(type))
            {
                var repoType = new GenericRepository<T>(_dbContext);
                _repo.Add(type, repoType);
            }
            return (IGenericRepository<T>)_repo[type];
        }
        public IUserRepository UserRepository { get;}
        public IResumeRepository ResumeRepository { get; }
        public IExperienceRepository ExperienceRepository { get; }
        public ISkillRepositroy SkillRepositroy { get; }

        public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
    }
}
