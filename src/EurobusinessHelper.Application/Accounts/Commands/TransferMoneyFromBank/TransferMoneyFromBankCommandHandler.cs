using System.Data;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Common.Models;
using EurobusinessHelper.Application.Identities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Accounts.Commands.TransferMoneyFromBank;

public class TransferMoneyFromBankCommandHandler : IRequestHandler<TransferMoneyFromBankCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IGameHubConnector _gameHubConnector;

    public TransferMoneyFromBankCommandHandler(IApplicationDbContext dbContext, IGameHubConnector gameHubConnector)
    {
        _dbContext = dbContext;
        _gameHubConnector = gameHubConnector;
    }
    public async Task<Unit> Handle(TransferMoneyFromBankCommand request, CancellationToken cancellationToken)
    {
        await ValidateCommand(request, cancellationToken);

        await UpdateAccountBalance(request, cancellationToken);

        await NotifyGameChanged(request, cancellationToken);

        return Unit.Value;
    }

    private async Task NotifyGameChanged(TransferMoneyFromBankCommand request, CancellationToken cancellationToken)
    {
        var gameId = await _dbContext.Accounts
            .Where(a => a.Id == request.ToAccount)
            .Select(a => a.Game.Id)
            .FirstOrDefaultAsync(cancellationToken);
        await _gameHubConnector.SendGameChangedNotifications(gameId);
        await _gameHubConnector.CreateOperationLog(GameOperationLog.BankTransferCompleted, gameId,
            request.ToAccount, request.Amount, null);
    }

    private async Task UpdateAccountBalance(TransferMoneyFromBankCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = _dbContext.BeginTransaction(IsolationLevel.Serializable);
        
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.ToAccount, cancellationToken);
        
        account.Balance += request.Amount;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private async Task ValidateCommand(TransferMoneyFromBankCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .AnyAsync(a => a.Id == request.ToAccount, cancellationToken);
        if (!account)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotFound,
                $"Account {request.ToAccount} not found.");
    }
}