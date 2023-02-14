using System.Collections.Concurrent;
using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs;

/// <summary>
/// Notification hub
/// </summary>
public class GameHub : Hub
{
    private readonly IConnectedAccountsManager _connectedAccountsManager;
    private readonly IMediator _mediator;

    /// <summary>
    /// Notification hub
    /// </summary>
    /// <param name="connectedAccountsManager"></param>
    /// <param name="mediator"></param>
    public GameHub(IConnectedAccountsManager connectedAccountsManager, IMediator mediator)
    {
        _connectedAccountsManager = connectedAccountsManager;
        _mediator = mediator;
    }
    /// <summary>
    /// Register accountId
    /// </summary>
    /// <param name="accountId"></param>
    public async Task RegisterAccount(Guid accountId)
    {
        var connectionId = Context.ConnectionId;
        var gameId = await GetGameId(accountId);
        _connectedAccountsManager.AddAccount(gameId, accountId, connectionId);
    }

    private async Task<Guid> GetGameId(Guid accountId)
    {
        return await _mediator.Send(new GetAccountGameQuery {AccountId = accountId});
    }

    /// <summary>
    /// Request bank transfer
    /// </summary>
    /// <param name="amount"></param>
    public async Task RequestBankTransfer(int amount)
    {
        var currentAccountId = GetCurrentAccountId();
        var requestId = await CreateTransferRequest(amount, currentAccountId);
        var gameId = await GetGameId(currentAccountId);
        
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Where(c => c.Key != currentAccountId)
            .Select(c => Clients.Client(c.Value));
        var tasks = clients.Select(c => SendTransferAcceptanceRequest(c, currentAccountId, amount, requestId));
        
        await Task.WhenAll(tasks);
    }

    private async Task<Guid> CreateTransferRequest(int amount, Guid currentAccountId)
    {
        var requestId = await _mediator.Send(new CreateTransferRequestCommand
            {
                AccountId = currentAccountId,
                Amount = amount
                
            });
        return requestId;
    }

    private Guid GetCurrentAccountId()
    {
        var currentAccountId = _connectedAccountsManager.GetAccountId(Context.ConnectionId);
        if (currentAccountId == default)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotRegistered,
                "Account not registered in the hub. First run 'RegisterAccount'");
        return currentAccountId;
    }

    private static Task SendTransferAcceptanceRequest(IClientProxy client, Guid accountId, int amount, Guid requestId)
    {
        return client.SendAsync(GameHubMethodNames.RequestBankTransferAcceptance, accountId, amount, requestId);
    }

    /// <summary>
    /// Remove accountId on disconnect
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;
        _connectedAccountsManager.RemoveConnection(connectionId);
        return base.OnDisconnectedAsync(exception);
    }
}
