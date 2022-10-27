using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Queries.GetActiveGamesQuery;
using EurobusinessHelper.Application.Identities.Commands.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.Application;

public static class DependencyInjection
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        return services
                //commands
                .AddTransient<ICreateIdentityCommandHandler, CreateIdentityCommandHandler>()
                .AddTransient<ICreateGameCommandHandler, CreateGameCommandHandler>()

                //queries
                .AddTransient<IGetIdentityByEmailQueryHandler, GetIdentityByEmailQueryHandler>()
                .AddTransient<IGetIdentityByIdQueryHandler, GetIdentityByIdQueryHandler>()
                .AddTransient<IGetActiveGamesQueryHandler, GetActiveGamesQueryHandler>()
            ;
    }

    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        
        config.NewConfig<Game, GetActiveGamesQueryResult>();
        config.NewConfig<Identity, IdentityDisplay>()
            .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}");
        config.NewConfig<Game, GetActiveGamesQueryResult.Item>()
            .Map(d => d.CreatedBy, s => $"{s.CreatedBy.FirstName} {s.CreatedBy.LastName} ({s.CreatedBy.Email})");

        return services.AddSingleton(config)
            .AddSingleton<IMapper, ServiceMapper>();
    }
}