using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Services;
using System.Reflection;

namespace MovieTagApp.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<ITagService, TagService>();
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<IMovieTagService, MovieTagService>();
        services.AddTransient<IParserService, ParserService>();
        services.AddTransient<IKinopoiskService, KinopoiskService>();
        services.AddTransient<IAddMovieRequestService, AddMovieRequestService>();        
        

        return services;
    }
}