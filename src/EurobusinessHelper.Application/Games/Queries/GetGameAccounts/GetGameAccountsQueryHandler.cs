using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameAccountsQueryHandler : IRequestHandler<GetGameAccountsQuery, GetGameAccountsQueryResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TypeAdapterConfig _mapperConfig;
    private readonly ISecurityContext _securityContext;

    public GetGameAccountsQueryHandler(IApplicationDbContext dbContext, ISecurityContext securityContext,
        TypeAdapterConfig mapperConfig)
    {
        _dbContext = dbContext;
        _securityContext = securityContext;
        _mapperConfig = mapperConfig;
    }

    public async Task<GetGameAccountsQueryResult> Handle(GetGameAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.Accounts)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        await Validate(game, request);
        var items = _dbContext.Accounts.Where(a => a.Game.Id == game.Id);
        return new GetGameAccountsQueryResult
        {
            Accounts = await items.ProjectToType<GetGameAccountsQueryResult.Item>(_mapperConfig)
                .ToListAsync(cancellationToken)
        };
    }

    private async Task Validate(Game game, GetGameAccountsQuery request)
    {
        if (game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound, $"Game {request.GameId} not found");

        var currentIdentity = await _securityContext.GetCurrentIdentity();
        if (game.Accounts.All(a => a.Owner.Id != currentIdentity.Id))
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"User {currentIdentity.Email} does not have an account in game {request.GameId}");
    }
}