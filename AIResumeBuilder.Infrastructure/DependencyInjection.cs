using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Infrastructure.Data;
using AIResumeBuilder.Infrastructure.Repositories;
using AIResumeBuilder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            return services;
        }
    }
}
