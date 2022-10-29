using EurobusinessHelper.Application.Common.Interfaces;
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
        if (query.Query != default)
            dbQuery = dbQuery.Where(g => g.Name.Contains(query.Query));

        return new GetActiveGamesQueryResult
        {
            Items = await dbQuery.ProjectToType<GetActiveGamesQueryResult.Item>(_mapperConfig).ToListAsync(cancellationToken)
        };
    }
}