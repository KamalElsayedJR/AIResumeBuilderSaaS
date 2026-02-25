using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUserByEmailAsync(string Email)
        =>  await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
        
    }
}
