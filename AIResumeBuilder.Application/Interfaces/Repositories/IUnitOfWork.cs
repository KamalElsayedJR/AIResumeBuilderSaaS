using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync();
        public IExperienceRepository ExperienceRepository { get; }
        public IResumeRepository ResumeRepository { get; }
        public IUserRepository UserRepository { get; }
        public ISkillRepositroy SkillRepositroy { get; }
        public IEducationRepository EducationRepository { get; }
        public IGeneratedResumesRepository GeneratedResumesRepository { get; }
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
