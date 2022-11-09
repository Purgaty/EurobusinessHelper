using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;

internal class GetIdentityByEmailQueryHandler : IRequestHandler<GetIdentityByEmailQuery, Identity>
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdentityByEmailQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Identity> Handle(GetIdentityByEmailQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Identities.FirstOrDefaultAsync(i => i.Email == query.Email, cancellationToken);
    }
}