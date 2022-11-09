using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;

namespace EurobusinessHelper.Application.Identities.Commands.CreateIdentity;

internal class CreateIdentityCommandHandler : IRequestHandler<CreateIdentityCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateIdentityCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Handle(CreateIdentityCommand command, CancellationToken cancellationToken)
    {
        //todo configure mapper
        var entity = new Domain.Entities.Identity
        {
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        _dbContext.Identities.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}