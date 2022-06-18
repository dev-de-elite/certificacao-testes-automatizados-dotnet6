using DeckGenerator.Application.Interfaces.Repositories;
using DeckGenerator.Infastructure.Context;
using DeckGenerator.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeckGenerator.Infastructure.DependecyInjection;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructureDependecies(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DeckGenerator"));
        services.AddTransient<IDeckRepository, DeckRepository>();
    }
}