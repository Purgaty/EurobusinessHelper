namespace EurobusinessHelper.UI.ASP.Hubs.Game;

public class ConnectedAccountsManager : IConnectedAccountsManager
{
    private static readonly object _lock = new();
    private readonly List<ConnectedAccount> _connectedAccounts = new();

    public void AddAccount(Guid gameId, Guid accountId, string connectionId)
    {
        lock (_lock)
        {
            var currentConnections = _connectedAccounts
                .Where(ca => ca.ConnectionId == connectionId || ca.AccountId == accountId)
                .ToList();
            if (currentConnections.Any())
                currentConnections.ForEach(cc => _connectedAccounts.Remove(cc));
            _connectedAccounts.Add(new ConnectedAccount(connectionId, gameId, accountId));
        }
    }

    public void AddGame(Guid gameId, string connectionId)
    {
        lock (_lock)
        {
            var currentConnection = _connectedAccounts
                .FirstOrDefault(ca => ca.ConnectionId == connectionId);
            if (currentConnection != default)
                _connectedAccounts.Remove(currentConnection);
            _connectedAccounts.Add(new ConnectedAccount(connectionId, gameId, null));
        }
    }

    public ICollection<ConnectedAccount> GetGameAccounts(Guid gameId)
    {
        lock (_lock)
        {
            return _connectedAccounts
                .Where(a => a.GameId == gameId)
                .ToList();
        }
    }

    public void RemoveConnection(string connectionId)
    {
        lock (_lock)
        {
            _connectedAccounts.RemoveAll(ca => ca.ConnectionId == connectionId);
        }
    }
}