using EurobusinessHelper.Application.Accounts.Commands.TransferMoney;
using EurobusinessHelper.Application.Accounts.Commands.TransferMoneyFromBank;
using EurobusinessHelper.Application.Accounts.Queries.CheckAccountGameAccess;
using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
using EurobusinessHelper.Application.Common.Utilities.PasswordHasher;
using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Commands.DeleteGame;
using EurobusinessHelper.Application.Games.Commands.JoinGame;
using EurobusinessHelper.Application.Games.Commands.UpdateGameState;
using EurobusinessHelper.Application.Games.Queries.GetActiveGames;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.Application.Games.Queries.GetIdentityGames;
using EurobusinessHelper.Application.Identities.Commands.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Application.TransferRequest.Commands.ApproveRequest;
using EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;
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
                .AddTransient<IRequestHandler<TransferMoneyCommand, Unit>, TransferMoneyCommandHandler>()
                .AddTransient<IRequestHandler<TransferMoneyFromBankCommand, Unit>, TransferMoneyFromBankCommandHandler>()
                .AddTransient<IRequestHandler<CreateTransferRequestCommand, Guid>, CreateTransferRequestCommandHandler>()
                .AddTransient<IRequestHandler<ApproveRequestCommand, Unit>, ApproveRequestCommandHandler>()

                //queries
                .AddTransient<IRequestHandler<GetIdentityByEmailQuery, Identity>, GetIdentityByEmailQueryHandler>()
                .AddTransient<IRequestHandler<GetIdentityByIdQuery, Identity>, GetIdentityByIdQueryHandler>()
                .AddTransient<IRequestHandler<GetActiveGamesQuery, GetActiveGamesQueryResult>, GetActiveGamesQueryHandler>()
                .AddTransient<IRequestHandler<GetGameDetailsQuery, GetGameDetailsQueryResult>, GetGameDetailsQueryHandler>()
                .AddTransient<IRequestHandler<GetIdentityGamesQuery, GetIdentityGamesQueryResult>, GetIdentityGamesQueryHandler>()
                .AddTransient<IRequestHandler<CheckAccountGameAccessQuery, bool>, CheckAccountGameAccessQueryHandler>()
                .AddTransient<IRequestHandler<GetAccountGameQuery, Guid>, GetAccountGameQueryHandler>()
            
                //utilities
                .AddTransient<IPasswordHasher, PasswordHasher>()
            ;
    }

    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        
        config.NewConfig<Identity, IdentityDisplay>()
            .Map(d => d.Name, s => $"{s.FirstName} {s.LastName}");
        config.NewConfig<Account, GetGameDetailsQueryResult.Account>()
            .Map(a => a.Name, a => a.Owner.FirstName + " " + a.Owner.LastName)
            .Map(a => a.Email, a => a.Owner.Email);

        return services.AddSingleton(config)
            .AddSingleton<IMapper, ServiceMapper>();
    }
}