using EurobusinessHelper.Domain.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.UI.ASP;

public static class DependencyInjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppConfig>(options => configuration.GetSection(nameof(AppConfig)).Bind(options))
            ;
        return services;
    }
}