using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityById;

internal class GetIdentityByIdQueryHandler : IRequestHandler<GetIdentityByIdQuery, Identity>
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdentityByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Identity> Handle(GetIdentityByIdQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Identities.FirstOrDefaultAsync(i => i.Id == query.Id, cancellationToken);
    }
}
