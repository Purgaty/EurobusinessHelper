using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using EurobusinessHelper.UI.ASP.Hubs.Game;
using Microsoft.AspNetCore.SignalR;

namespace EurobusinessHelper.UI.ASP.Hubs.Main;

public class MainHubConnector : IMainHubConnector
{
    private readonly IHubContext<MainHub> _hubContext;

    public MainHubConnector(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task SendGameListChangedNotifications(GameState state)
    {
        await _hubContext.Clients.All.SendAsync(MainHubMethodNames.GameListChangedNotification, state);
    }
}