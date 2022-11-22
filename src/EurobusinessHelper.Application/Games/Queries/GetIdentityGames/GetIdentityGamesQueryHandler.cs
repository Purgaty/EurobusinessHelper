using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Queries.GetIdentityGames;

public class GetIdentityGamesQueryHandler : IRequestHandler<GetIdentityGamesQuery, GetIdentityGamesQueryResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdentityGamesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetIdentityGamesQueryResult> Handle(GetIdentityGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.Games
            .Where(g => g.IsActive && g.Accounts.Any(a => a.Owner.Id == request.IdentityId))
            .Select(g => g.Id)
            .ToListAsync(cancellationToken);

        return new GetIdentityGamesQueryResult
        {
            Games = games
        };
    }
}