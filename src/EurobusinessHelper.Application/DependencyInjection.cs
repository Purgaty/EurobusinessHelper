using EurobusinessHelper.Application.Common.Utilities.PasswordHasher;
using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Commands.CreateGameAccount;
using EurobusinessHelper.Application.Games.Commands.DeleteGame;
using EurobusinessHelper.Application.Games.Commands.UpdateGameState;
using EurobusinessHelper.Application.Games.Queries.GetActiveGames;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.Application.Games.Queries.GetIdentityGames;
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
                .AddTransient<IRequestHandler<JoinGameCommand, Unit>, JoinGameCommandHandler>()
                .AddTransient<IRequestHandler<DeleteGameCommand, Unit>, DeleteGameCommandHandler>()
                .AddTransient<IRequestHandler<UpdateGameStateCommand, Unit>, UpdateGameStateCommandHandler>()

                //queries
                .AddTransient<IRequestHandler<GetIdentityByEmailQuery, Identity>, GetIdentityByEmailQueryHandler>()
                .AddTransient<IRequestHandler<GetIdentityByIdQuery, Identity>, GetIdentityByIdQueryHandler>()
                .AddTransient<IRequestHandler<GetActiveGamesQuery, GetActiveGamesQueryResult>, GetActiveGamesQueryHandler>()
                .AddTransient<IRequestHandler<GetGameDetailsQuery, GetGameDetailsQueryResult>, GetGameDetailsQueryHandler>()
                .AddTransient<IRequestHandler<GetIdentityGamesQuery, GetIdentityGamesQueryResult>, GetIdentityGamesQueryHandler>()
            
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

        return services.AddSingleton(config)
            .AddSingleton<IMapper, ServiceMapper>();
    }
}