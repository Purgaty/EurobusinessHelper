using System.Collections.Concurrent;

namespace EurobusinessHelper.UI.ASP.Hubs.Game;

public interface IConnectedAccountsManager
{
    public void AddAccount(Guid gameId, Guid accountId, string connectionId);
    public void AddGame(Guid gameId, string connectionId);
    ICollection<ConnectedAccount> GetGameAccounts(Guid gameId);
    void RemoveConnection(string connectionId);
}