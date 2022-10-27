using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Games.Commands.CreateGame;

public class CreateGameCommandHandler : ICreateGameCommandHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISecurityContext _securityContext;

    public CreateGameCommandHandler(IApplicationDbContext dbContext, ISecurityContext securityContext)
    {
        _dbContext = dbContext;
        _securityContext = securityContext;
    }
    public async Task<Guid> Handle(CreateGameCommand command)
    {
        var entity = new Game
        {
            Name = command.Name,
            CreatedBy = await _securityContext.GetCurrentIdentity()
        };
        _dbContext.Games.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }
}