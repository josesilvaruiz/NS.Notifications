using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NS.Notifications.App.Contracts.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddAppContracts(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
