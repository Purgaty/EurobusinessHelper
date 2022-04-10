using EurobusinessHelper.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;

internal class GetIdentityByEmailQueryHandler : IGetIdentityByEmailQueryHandler
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdentityByEmailQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Identity?> Handle(GetIdentityByEmailQuery query)
    {
        return await _dbContext.Identities.FirstOrDefaultAsync(i => i.Email == query.Email);
    }
}
