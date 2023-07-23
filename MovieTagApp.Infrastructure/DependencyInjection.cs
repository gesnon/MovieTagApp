using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieTagAppContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMovieTagAppContext>(provider => provider.GetRequiredService<MovieTagAppContext>());
            //services.AddScoped<IKinopoiskService>(provider => provider.GetRequiredService<IKinopoiskService>());
            services.AddScoped<DbInitializer>();         
            

            return services;
        }
    }
}
