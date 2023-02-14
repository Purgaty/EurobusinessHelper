using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;

public class GetAccountGameQueryHandler : IRequestHandler<GetAccountGameQuery, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAccountGameQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Handle(GetAccountGameQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .Where(a => a.Id == request.AccountId)
            .Select(a => a.Game.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (account == default)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotFound,
                $"Account {request.AccountId} not found");

        return account;
    }
}