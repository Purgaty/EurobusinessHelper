using EurobusinessHelper.Application.Common.Utilities.PasswordHasher;
using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Commands.CreateGameAccount;
using EurobusinessHelper.Application.Games.Queries.GetActiveGames;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.Application.Identities.Commands.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.Application;

public static class DependencyInjection
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        return services
                //commands
                .AddTransient<IRequestHandler<CreateIdentityCommand, Guid>, CreateIdentityCommandHandler>()
                .AddTransient<IRequestHandler<CreateGameCommand, Guid>, CreateGameCommandHandler>()
                .AddTransient<IRequestHandler<CreateGameAccountCommand, Unit>, CreateGameAccountCommandHandler>()

                //queries
                .AddTransient<IRequestHandler<GetIdentityByEmailQuery, Identity>, GetIdentityByEmailQueryHandler>()
                .AddTransient<IRequestHandler<GetIdentityByIdQuery, Identity>, GetIdentityByIdQueryHandler>()
                .AddTransient<IRequestHandler<GetActiveGamesQuery, GetActiveGamesQueryResult>, GetActiveGamesQueryHandler>()
                .AddTransient<IRequestHandler<GetGameAccountsQuery, GetGameAccountsQueryResult>, GetGameAccountsQueryHandler>()
            
                //utilities
                .AddTransient<IPasswordHasher, PasswordHasher>()
            ;
    }

    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        
        config.NewConfig<Game, GetActiveGamesQueryResult>();
        config.NewConfig<Identity, IdentityDisplay>()
            .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}");
        config.NewConfig<Game, GetActiveGamesQueryResult.Item>()
            .Map(d => d.CreatedBy, s => $"{s.CreatedBy.FirstName} {s.CreatedBy.LastName} ({s.CreatedBy.Email})")
            .Map(d => d.AccountCount, s => s.Accounts.Count);

        return services.AddSingleton(config)
            .AddSingleton<IMapper, ServiceMapper>();
    }
}