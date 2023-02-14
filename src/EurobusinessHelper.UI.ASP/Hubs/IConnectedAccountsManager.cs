using System.Collections.Concurrent;

namespace EurobusinessHelper.UI.ASP.Hubs;

public interface IConnectedAccountsManager
{
    public void AddAccount(Guid gameId, Guid accountId, string connectionId);
    void RemoveAccount(Guid gameId, Guid accountId);
    Guid GetAccountId(string connectionId);
    ConcurrentDictionary<Guid, string> GetGameAccounts(Guid gameId);
    void RemoveConnection(string connectionId);
}