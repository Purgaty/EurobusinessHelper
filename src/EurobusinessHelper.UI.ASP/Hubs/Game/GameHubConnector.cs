using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs.Game;

public class GameHubConnector : IGameHubConnector
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly IConnectedAccountsManager _connectedAccountsManager;
    private readonly IMediator _mediator;

    public GameHubConnector(IHubContext<GameHub> hubContext, IConnectedAccountsManager connectedAccountsManager, IMediator mediator)
    {
        _hubContext = hubContext;
        _connectedAccountsManager = connectedAccountsManager;
        _mediator = mediator;
    }

    public async Task SendGameChangedNotifications(Guid gameId)
    {
        var clients = _connectedAccountsManager
            .GetGameAccounts(gameId)
            .Select(a => _hubContext.Clients.Client(a.ConnectionId));
        var tasks = clients.Select(NotifyGameChanged);
        await Task.WhenAll(tasks);
    }

    public async Task RequestBankTransferApprovals(Guid requestId, Guid gameId, Guid accountId, int amount)
    {
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Where(a => a.AccountId != accountId)
            .Select(c => _hubContext.Clients.Client(c.ConnectionId));
        var tasks = clients.Select(c => RequestBankTransferApproval(c, requestId, accountId, amount));
        await Task.WhenAll(tasks);
    }

    public async Task RequestMoneyTransfer(Guid gameId, Guid fromAccount, Guid toAccount, int amount)
    {
        var connectionId = _connectedAccountsManager
            .GetGameAccounts(gameId)
            .Where(c => c.AccountId == toAccount)
            .Select(c => c.ConnectionId)
            .FirstOrDefault();
        if (connectionId == default)
            throw new EurobusinessException(EurobusinessExceptionCode.AccountNotRegistered,
                $"Account {toAccount} is not registered.");
        await _hubContext.Clients.Client(connectionId).SendAsync(GameHubMethodNames.RequestMoneyTransfer, fromAccount, amount);
    }

    private static async Task RequestBankTransferApproval(IClientProxy client, Guid requestId, Guid accountId, int amount)
    {
        await client.SendAsync(GameHubMethodNames.RequestBankTransferApproval, requestId, accountId, amount);
    }

    private static Task NotifyGameChanged(IClientProxy client)
    {
        return client.SendAsync(GameHubMethodNames.GameChangedNotification);
    }

    private async Task<Guid> GetAccountGameId(Guid accountId)
    {
        var query = new GetAccountGameQuery {AccountId = accountId};
        return await _mediator.Send(query);
    }
}