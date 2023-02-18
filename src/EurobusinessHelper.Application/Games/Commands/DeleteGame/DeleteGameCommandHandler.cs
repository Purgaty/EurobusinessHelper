using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Commands.DeleteGame;

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMainHubConnector _mainHubConnector;
    private readonly ISecurityContext _securityContext;

    public DeleteGameCommandHandler(ISecurityContext securityContext, IApplicationDbContext dbContext,
        IMainHubConnector mainHubConnector)
    {
        _securityContext = securityContext;
        _dbContext = dbContext;
        _mainHubConnector = mainHubConnector;
    }

    public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games
            .Include(g => g.CreatedBy)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

        await ValidateCommand(request, game);

        game!.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _mainHubConnector.SendGameListChangedNotifications(game.State);
        return Unit.Value;
    }

    private async Task ValidateCommand(DeleteGameCommand request, Game game)
    {
        if (game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound,
                $"Game {request.GameId} not found.");
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        if (game.CreatedBy.Id != currentIdentity.Id)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Identity {currentIdentity.Email} isn't the owner of game {game.Name}");
    }
}