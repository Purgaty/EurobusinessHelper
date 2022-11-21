using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GetGameDetailsQueryResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGameDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetGameDetailsQueryResult> Handle(GetGameDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.Accounts)
            .Include(g => g.CreatedBy)
            .FirstOrDefaultAsync(g => g.IsActive && g.Id == request.GameId, cancellationToken);
        
        if (game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound, $"Game {request.GameId} not found");
        
        return _mapper.Map<GetGameDetailsQueryResult>(game);
    }
}