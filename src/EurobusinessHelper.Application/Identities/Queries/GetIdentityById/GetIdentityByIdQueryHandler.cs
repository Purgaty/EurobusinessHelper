using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityById;

internal class GetIdentityByIdQueryHandler : IGetIdentityByIdQueryHandler
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdentityByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Identity?> Handle(GetIdentityByIdQuery query)
    {
        return await _dbContext.Identities.FindAsync(query.Id);
    }
}
