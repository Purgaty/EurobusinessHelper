namespace EurobusinessHelper.Application.Common.Interfaces;

public interface IGameHubConnector
{
    Task SendGameChangedNotifications(Guid gameId);
    Task RequestBankTransferApprovals(Guid requestId, Guid gameId, Guid accountId, int amount);
    Task RequestMoneyTransfer(Guid gameId, Guid fromAccount, Guid toAccount, int amount);
}