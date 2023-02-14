namespace EurobusinessHelper.Application.Common.Interfaces;

public interface IHubConnector
{
    Task SendAccountTransferNotifications(Guid accountFrom, Guid accountTo, int amount);
    Task SendBankTransferNotifications(Guid accountTo, int amount);
}