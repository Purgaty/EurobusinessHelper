using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Common.Interfaces;

public interface IMainHubConnector
{
    Task SendGameListChangedNotifications(GameState state);
}