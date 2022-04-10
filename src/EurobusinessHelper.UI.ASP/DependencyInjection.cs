using EurobusinessHelper.Application;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.UI.ASP;

public static class DependencyInjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                .Configure<AppConfig>(options => configuration.GetSection(nameof(AppConfig)).Bind(options))
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<ISecurityContext, SecurityContext>()
                .InjectApplicationDependencies()
                .InjectInfrastructureDependencies(configuration)
            ;
    }
}