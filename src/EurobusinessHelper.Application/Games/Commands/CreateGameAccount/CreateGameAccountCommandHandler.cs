using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Common.Utilities.PasswordHasher;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Games.Commands.CreateGameAccount;

public class CreateGameAccountCommandHandler : IRequestHandler<CreateGameAccountCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityContext _securityContext;

    public CreateGameAccountCommandHandler(ISecurityContext securityContext, IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher)
    {
        _securityContext = securityContext;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(CreateGameAccountCommand request, CancellationToken cancellationToken)
    {
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        var game = await _dbContext.Games
            .Include(g => g.Accounts)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        Validate(request, game, currentIdentity);
        game!.Accounts.Add(new Account
        {
            Owner = currentIdentity
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private void Validate(CreateGameAccountCommand request, Game game, Identity currentIdentity)
    {
        if (game == default)
            throw new EurobusinessException(EurobusinessExceptionCode.GameNotFound,
                $"Game {request.GameId} not found");

        if (game.Accounts.Any(a => a.Owner.Id == currentIdentity.Id))
            throw new EurobusinessException(EurobusinessExceptionCode.AccountAlreadyExists,
                $"User {currentIdentity.Email} already has an account in game {game.Id}");

        if (game.IsPasswordProtected && !_passwordHasher.ValidatePassword(request.Password, game.Password))
            throw new EurobusinessException(EurobusinessExceptionCode.InvalidGamePassword,
                $"Provided game password is invalid");
    }
}