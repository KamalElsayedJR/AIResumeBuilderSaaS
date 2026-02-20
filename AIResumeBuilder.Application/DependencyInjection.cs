using AIResumeBuilder.Application.Mapping;
using AIResumeBuilder.Application.UseCase.Auth;
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
            services.AddScoped<RegisterHandler>();
            services.AddScoped<LoginHandler>();
            return services;
        }
    }
}
