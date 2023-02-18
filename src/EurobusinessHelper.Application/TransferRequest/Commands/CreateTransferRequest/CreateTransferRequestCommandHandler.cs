using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;

public class CreateTransferRequestCommandHandler : IRequestHandler<CreateTransferRequestCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IGameHubConnector _gameHubConnector;

    public CreateTransferRequestCommandHandler(IApplicationDbContext dbContext, IGameHubConnector gameHubConnector)
    {
        _dbContext = dbContext;
        _gameHubConnector = gameHubConnector;
    }

    public async Task<Guid> Handle(CreateTransferRequestCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .Include(a => a.Game)
            .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
        if (account == default)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotFound,
                $"Account {request.AccountId} not found");
        var entity = new Domain.Entities.TransferRequest
        {
            Account = account,
            Amount = request.Amount
        };

        _dbContext.TransferRequest.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _gameHubConnector.RequestBankTransferApprovals(entity.Id, account.Game.Id, account.Id, entity.Amount);
        return entity.Id;
    }
}