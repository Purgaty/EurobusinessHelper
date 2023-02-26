using System.Data;
using EurobusinessHelper.Application.Accounts.Commands.TransferMoneyFromBank;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.TransferRequest.Commands.ApproveRequest;

public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISecurityContext _securityContext;
    private readonly IMediator _mediator;

    public ApproveRequestCommandHandler(IApplicationDbContext dbContext, ISecurityContext securityContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _securityContext = securityContext;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        var transferRequest = await ApproveTransferRequest(request.RequestId, cancellationToken);
        if (transferRequest.Approved)
            await CompleteTransfer(transferRequest);
        
        return Unit.Value;
    }

    private async Task CompleteTransfer(Domain.Entities.TransferRequest request)
    {
        var command = new TransferMoneyFromBankCommand
        {
            ToAccount = request.Account.Id,
            Amount = request.Amount
        };
        await _mediator.Send(command);
    }

    private async Task<Domain.Entities.TransferRequest> ApproveTransferRequest(Guid requestId, CancellationToken cancellationToken)
    {
        var approvalsNeeded = await ApprovalsNeeded(requestId, cancellationToken);
        Domain.Entities.TransferRequest request;
        await using var transaction = _dbContext.BeginTransaction(IsolationLevel.Serializable);
        request = await _dbContext.TransferRequest
            .Include(r => r.Account)
            .FirstAsync(r => r.Id == requestId, cancellationToken);
        request.ApprovalCount++;
        if (request.ApprovalCount == approvalsNeeded)
            request.Approved = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return request;
    }

    private async Task<int> ApprovalsNeeded(Guid requestId, CancellationToken cancellationToken)
    {
        return await _dbContext.TransferRequest
            .Where(r => r.Id == requestId)
            .Select(r => r.Account.Game.MinimalBankTransferApprovals)
            .FirstAsync(cancellationToken);
    }

    private async Task ValidateRequest(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        var gameId = await GetRequestGame(request, cancellationToken);
        var currentIdentity = await _securityContext.GetCurrentIdentity();
        var hasAccess = await _dbContext.Games
            .AnyAsync(g =>
                    g.Id == gameId &&
                    g.Accounts.Any(a =>
                        a.Owner.Id == currentIdentity.Id),
                cancellationToken);
        if (!hasAccess)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Identity {currentIdentity.Email} can't accept request {request.RequestId}");
    }

    private async Task<Guid> GetRequestGame(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        var requestGame = await _dbContext.TransferRequest
            .Where(t => t.Id == request.RequestId)
            .Select(t => t.Account.Game.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (requestGame == default)
            throw new EurobusinessException(EurobusinessExceptionCode.TransferRequestNotFound,
                $"Transfer request {request.RequestId} not found");

        return requestGame;
    }
}