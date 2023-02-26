using EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs.Game;

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
    
    /// <summary>
    /// Register gameId
    /// </summary>
    /// <param name="gameId"></param>
    public void RegisterGame(Guid gameId)
    {
        var connectionId = Context.ConnectionId;
        _connectedAccountsManager.AddGame(gameId, connectionId);
    }

    private async Task<Guid> GetGameId(Guid accountId)
    {
        return await _mediator.Send(new GetAccountGameQuery {AccountId = accountId});
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
