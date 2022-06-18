using DeckGenerator.Application.Automapper;
using DeckGenerator.Application.Services;
using DeckGenerator.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeckGenerator.Application.DependecyInjection;

public static class DependecyInjectionExtension
{
    public static void AddApplicationDependecies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EntityToDtoProfile));

        services.AddMemoryCache();
        
        services.AddTransient<IDeckService, DeckService>();
        services.AddTransient<IDeckGeneratorService, DeckGeneratorService>();
    }
}