using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
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
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Select(c => _hubContext.Clients.Client(c.Value));
        var tasks = clients.Select(NotifyGameChanged);
        await Task.WhenAll(tasks);
    }

    public async Task RequestBankTransferApprovals(Guid requestId, Guid gameId, Guid accountId, int amount)
    {
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Where(a => a.Key != accountId)
            .Select(c => _hubContext.Clients.Client(c.Value));
        var tasks = clients.Select(c => RequestBankTransferApproval(c, requestId, accountId, amount));
        await Task.WhenAll(tasks);
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