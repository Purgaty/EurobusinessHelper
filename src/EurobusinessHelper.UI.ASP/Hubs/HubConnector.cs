using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs;

public class HubConnector : IHubConnector
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly IConnectedAccountsManager _connectedAccountsManager;
    private readonly IMediator _mediator;

    public HubConnector(IHubContext<GameHub> hubContext, IConnectedAccountsManager connectedAccountsManager, IMediator mediator)
    {
        _hubContext = hubContext;
        _connectedAccountsManager = connectedAccountsManager;
        _mediator = mediator;
    }
    
    public async Task SendAccountTransferNotifications(Guid accountFrom, Guid accountTo, int amount)
    {
        var gameId = await GetAccountGameId(accountFrom);
        
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Select(c => _hubContext.Clients.Client(c.Value));
        var tasks = clients.Select(c => SendAccountTransferNotification(c, accountFrom, accountTo, amount));
        await Task.WhenAll(tasks);
    }

    public async Task SendBankTransferNotifications(Guid accountTo, int amount)
    {
        var gameId = await GetAccountGameId(accountTo);
        
        var clients = _connectedAccountsManager.GetGameAccounts(gameId)
            .Select(c => _hubContext.Clients.Client(c.Value));
        var tasks = clients.Select(c => SendBankTransferNotification(c, accountTo, amount));
        await Task.WhenAll(tasks);
    }

    private static Task SendAccountTransferNotification(IClientProxy client, Guid accountFrom, Guid accountTo, int amount)
    {
        return client.SendAsync(GameHubMethodNames.AccountTransferNotification, accountFrom, accountTo, amount);
    }

    private static Task SendBankTransferNotification(IClientProxy client, Guid accountTo, int amount)
    {
        return client.SendAsync(GameHubMethodNames.BankTransferNotification, accountTo, amount);
    }

    private async Task<Guid> GetAccountGameId(Guid accountId)
    {
        var query = new GetAccountGameQuery {AccountId = accountId};
        return await _mediator.Send(query);
    }
}