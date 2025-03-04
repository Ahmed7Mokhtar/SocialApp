using Application.ServiceInterfaces;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceRegisteration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
