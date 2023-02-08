using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EurobusinessHelper.Application.Games.Commands.UpdateGameState;

public class UpdateGameStateCommandHandler : IRequestHandler<UpdateGameStateCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISecurityContext _securityContext;
    private readonly AppConfig _appConfig;

    public UpdateGameStateCommandHandler(IApplicationDbContext dbContext, ISecurityContext securityContext, IOptions<AppConfig> appConfig)
    {
        _dbContext = dbContext;
        _securityContext = securityContext;
        _appConfig = appConfig.Value;
    }
    
    public async Task<Unit> Handle(UpdateGameStateCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.CreatedBy)
            .Include(g => g.Accounts)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        await ValidateCommand(request, game);
        UpdateGame(request, game);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private void UpdateGame(UpdateGameStateCommand request, Game game)
    {
        new GameStateStateMachine(game).SetState(request.State);
        if (game.State != GameState.Started)
            return;
        foreach (var gameAccount in game.Accounts)
            gameAccount.Balance = game.StartingAccountBalance;
    }

    private async Task ValidateCommand(UpdateGameStateCommand request, Game game)
    {
        if(game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound,
                $"Game {request.GameId} not found.");
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        if (game.CreatedBy.Id != currentIdentity.Id)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Account {currentIdentity.Email} is not the creator of game {game.Name}");
    }
}