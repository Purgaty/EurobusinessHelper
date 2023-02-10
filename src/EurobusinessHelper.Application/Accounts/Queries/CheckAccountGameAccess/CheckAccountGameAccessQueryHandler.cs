using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Accounts.Queries.CheckAccountGameAccess;

public class CheckAccountGameAccessQueryHandler : IRequestHandler<CheckAccountGameAccessQuery, bool>
{
    private readonly ISecurityContext _securityContext;
    private readonly IApplicationDbContext _dbContext;

    public CheckAccountGameAccessQueryHandler(ISecurityContext securityContext, IApplicationDbContext dbContext)
    {
        _securityContext = securityContext;
        _dbContext = dbContext;
    }
    public async Task<bool> Handle(CheckAccountGameAccessQuery request, CancellationToken cancellationToken)
    {
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        return await _dbContext.Accounts
            .AnyAsync(a =>
                a.Id == request.AccountId &&
                a.Owner.Id == currentIdentity.Id &&
                a.Game.Id == request.GameId, cancellationToken);
    }
}