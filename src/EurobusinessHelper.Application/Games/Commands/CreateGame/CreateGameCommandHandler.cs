using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Common.Utilities.PasswordHasher;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.CreateGame;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityContext _securityContext;

    public CreateGameCommandHandler(IApplicationDbContext dbContext, ISecurityContext securityContext,
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _securityContext = securityContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        Validate(command);
        var entity = new Game
        {
            Name = command.Name,
            IsPasswordProtected = command.IsPasswordProtected,
            Password = GetPasswordHash(command),
            CreatedBy = await _securityContext.GetCurrentIdentity()
        };
        _dbContext.Games.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private static void Validate(CreateGameCommand command)
    {
        if (command.IsPasswordProtected && string.IsNullOrWhiteSpace(command.Password))
            throw new EurobusinessException(EurobusinessExceptionCode.PasswordNotProvided,
                "Game protected by password must have a password set");
    }

    private string GetPasswordHash(CreateGameCommand command)
    {
        return command.IsPasswordProtected ? _passwordHasher.GetPasswordHash(command.Password) : default;
    }
}