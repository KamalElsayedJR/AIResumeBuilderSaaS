using AIResumeBuilder.Application.Mapping;
using AIResumeBuilder.Application.Services.Implementation;
using AIResumeBuilder.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddScoped<IResumeExperienceService, ResumeExperienceService>();
            return services;
        }
    }
}
