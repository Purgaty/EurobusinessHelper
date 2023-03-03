using System.Data;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Common.Models;
using EurobusinessHelper.Application.Identities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Accounts.Commands.TransferMoney;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand>
{
    private readonly ISecurityContext _securityContext;
    private readonly IApplicationDbContext _dbContext;
    private readonly IGameHubConnector _gameHubConnector;

    public TransferMoneyCommandHandler(ISecurityContext securityContext, IApplicationDbContext dbContext, IGameHubConnector gameHubConnector)
    {
        _securityContext = securityContext;
        _dbContext = dbContext;
        _gameHubConnector = gameHubConnector;
    }
    public async Task<Unit> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        await ValidateCommand(request, cancellationToken);

        await UpdateAccountBalance(request, cancellationToken);

        await _gameHubConnector.SendGameChangedNotifications(request.GameId);
        await _gameHubConnector.CreateOperationLog(GameOperationLog.TransferCompleted, request.GameId,
            request.ToAccount, request.Amount, request.FromAccount);
        
        return Unit.Value;
    }

    private async Task UpdateAccountBalance(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = _dbContext.BeginTransaction(IsolationLevel.Serializable);
        
        var accountFrom = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.FromAccount, cancellationToken);
        if (accountFrom.Balance < request.Amount)
            throw new EurobusinessException(EurobusinessExceptionCode.InsufficientFunds,
                $"Insufficient funds on account {accountFrom}");
        var accountTo = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.ToAccount, cancellationToken);
        
        accountFrom.Balance -= request.Amount;
        accountTo.Balance += request.Amount;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private async Task ValidateCommand(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        await ValidateAccount(request.ToAccount, request.GameId, cancellationToken);
        await ValidateAccount(request.FromAccount, request.GameId, cancellationToken);
        await ValidateAccountOwner(request.FromAccount, cancellationToken);
    }

    private async Task ValidateAccountOwner(Guid accountId, CancellationToken cancellationToken)
    {
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        var accountOwner = await _dbContext.Accounts
            .Where(a => a.Id == accountId)
            .Select(a => a.Owner.Id)
            .SingleOrDefaultAsync(cancellationToken);
        
        if(accountOwner != currentIdentity.Id)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Identity {currentIdentity.Email} isn't the owner of account {accountId}");
    }

    private async Task ValidateAccount(Guid accountId, Guid gameId, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .AnyAsync(a => a.Id == accountId && a.Game.Id == gameId, cancellationToken);
        if (!account)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotFound,
                $"Account {accountId} in game {gameId} not found.");
    }
}