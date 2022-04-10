using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Command.CreateIdentity;

internal class CreateIdentityCommandHandler : ICreateIdentityCommandHandler
{
    private readonly IApplicationDbContext _dbContext;

    public CreateIdentityCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Handle(CreateIdentityCommand command)
    {
        //todo configure mapper
        var entity = new Domain.Entities.Identity
        {
            Email = command.Email,
            Name = command.Name
        };

        _dbContext.Identities.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }
}