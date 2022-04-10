using EurobusinessHelper.Application.Identities.Command.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.Application;

public static class DependencyInjection
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        return services
                //commands
                .AddTransient<ICreateIdentityCommandHandler, CreateIdentityCommandHandler>()

                //queries
                .AddTransient<IGetIdentityByEmailQueryHandler, GetIdentityByEmailQueryHandler>()
                .AddTransient<IGetIdentityByIdQueryHandler, GetIdentityByIdQueryHandler>()
            ;
    }
}