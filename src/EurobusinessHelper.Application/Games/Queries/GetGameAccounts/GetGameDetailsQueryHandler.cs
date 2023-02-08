using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GetGameDetailsQueryResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ISecurityContext _securityContext;

    public GetGameDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper, ISecurityContext securityContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _securityContext = securityContext;
    }

    public async Task<GetGameDetailsQueryResult> Handle(GetGameDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.Accounts)
            .ThenInclude(a => a.Owner)
            .Include(g => g.CreatedBy)
            .FirstOrDefaultAsync(g => g.IsActive && g.Id == request.GameId, cancellationToken);
        
        if (game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound, $"Game {request.GameId} not found");
        
        var mappedGame = _mapper.Map<GetGameDetailsQueryResult>(game);
        mappedGame.CanJoin = await CanJoinGame(game);
        return mappedGame;
    }

    private async Task<bool> CanJoinGame(Game game)
    {
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        return game.Accounts.Any(a => a.Owner.Id == currentIdentity.Id);
    }
}