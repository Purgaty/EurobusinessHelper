using System.Collections.Concurrent;

namespace EurobusinessHelper.UI.ASP.Hubs;

public class ConnectedAccountsManager : IConnectedAccountsManager
{
    private ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, string>> _accounts;
    private ConcurrentDictionary<string, Guid> _connections;

    public ConnectedAccountsManager()
    {
        _accounts = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, string>>();
        _connections = new ConcurrentDictionary<string, Guid>();
    }
    
    public void AddAccount(Guid gameId, Guid accountId, string connectionId)
    {
        var gameAccounts = GetGameAccounts(gameId);
        gameAccounts.AddOrUpdate(accountId, _ => connectionId, (_, _) => connectionId);
        _connections.AddOrUpdate(connectionId, _ => gameId, (_, _) => gameId);
    }

    public void RemoveAccount(Guid gameId, Guid accountId)
    {
        var gameAccounts = GetGameAccounts(gameId);
        if (gameAccounts.TryRemove(accountId, out var connectionId))
            _connections.TryRemove(connectionId, out _);
    }

    public Guid GetAccountId(string connectionId)
    {
        var gameId = _connections[connectionId];
        var gameAccounts = GetGameAccounts(gameId);
        return gameAccounts
            .FirstOrDefault(g => g.Value == connectionId)
            .Key;
    }

    public ConcurrentDictionary<Guid, string> GetGameAccounts(Guid gameId)
    {
        return _accounts.GetOrAdd(gameId, _ => new ConcurrentDictionary<Guid, string>());
    }

    public void RemoveConnection(string connectionId)
    {
        if (_connections.TryRemove(connectionId, out var gameId))
        {
            var gameAccounts = GetGameAccounts(gameId);
            var account = gameAccounts
                .FirstOrDefault(a => a.Value == connectionId);
            gameAccounts.TryRemove(account);
        }
    }
}