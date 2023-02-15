using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Queries.GetActiveGames;

public class GetActiveGamesQueryHandler : IRequestHandler<GetActiveGamesQuery, GetActiveGamesQueryResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TypeAdapterConfig _mapperConfig;

    public GetActiveGamesQueryHandler(IApplicationDbContext dbContext, TypeAdapterConfig mapperConfig)
    {
        _dbContext = dbContext;
        _mapperConfig = mapperConfig;
    }
    public async Task<GetActiveGamesQueryResult> Handle(GetActiveGamesQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _dbContext.Games
            .Where(g => g.IsActive);
        
        dbQuery = dbQuery.Where(g => g.State == query.State);

        if (query.State != GameState.New)
            dbQuery = dbQuery.Where(g => g.Accounts.Any(a => a.Owner.Id == query.Participant.Id));
                
        if (query.Query != default)
            dbQuery = dbQuery.Where(g => g.Name.Contains(query.Query));

        return new GetActiveGamesQueryResult
        {
            Items = await dbQuery
                .OrderByDescending(q => q.CreatedOn)
                .ProjectToType<GetActiveGamesQueryResult.Item>(_mapperConfig)
                .ToListAsync(cancellationToken)
        };
    }
}