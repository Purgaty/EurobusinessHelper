using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs;

/// <summary>
/// Notification hub
/// </summary>
public class NotificationHub : Hub
{
    private readonly static ConcurrentDictionary<string, Guid> _connectedAccounts;

    static NotificationHub()
    {
        _connectedAccounts = new ConcurrentDictionary<string, Guid>();
    }
    
    /// <summary>
    /// Register accountId
    /// </summary>
    /// <param name="accountId"></param>
    public void RegisterAccount(Guid accountId)
    {
        var connectionId = Context.ConnectionId;
        _connectedAccounts.AddOrUpdate(connectionId, accountId, (_, _) => accountId);
    }

    /// <summary>
    /// Request bank transfer
    /// </summary>
    /// <param name="amount"></param>
    public async Task RequestBankTransfer(int amount)
    {
        var currentAccountId = _connectedAccounts[Context.ConnectionId];
        var requestId = Guid.NewGuid();

        var clients = _connectedAccounts.ToList()
            .Where(c => c.Value != currentAccountId)
            .Select(c => Clients.Client(c.Key));
        var tasks = clients.Select(c => SendRequestNotification(c, currentAccountId, amount, requestId));
        await Task.WhenAll(tasks);
    }

    private async Task SendRequestNotification(IClientProxy client, Guid accountId, int amount, Guid requestId)
    {
        await client.SendAsync("requestTransferNotification", accountId, amount, requestId);
    }

    /// <summary>
    /// Remove accountId on disconnect
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;
        _connectedAccounts.TryRemove(connectionId, out var _);
        return base.OnDisconnectedAsync(exception);
    }
}
