namespace EurobusinessHelper.UI.ASP.Hubs.Game;

public record ConnectedAccount(string ConnectionId, Guid GameId, Guid? AccountId);