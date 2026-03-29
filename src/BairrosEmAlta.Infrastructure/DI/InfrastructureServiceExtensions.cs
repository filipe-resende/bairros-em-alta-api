using BairrosEmAlta.Application.Interfaces;
using BairrosEmAlta.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BairrosEmAlta.Infrastructure.DI;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<INeighborhoodRepository, NeighborhoodMockRepository>();
        return services;
    }
}
