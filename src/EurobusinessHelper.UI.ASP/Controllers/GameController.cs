using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Queries.GetActiveGamesQuery;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly IGetActiveGamesQueryHandler _getActiveGamesQueryHandler;
    private readonly ICreateGameCommandHandler _createGameCommandHandler;

    public GameController(IGetActiveGamesQueryHandler getActiveGamesQueryHandler,
        ICreateGameCommandHandler createGameCommandHandler)
    {
        _getActiveGamesQueryHandler = getActiveGamesQueryHandler;
        _createGameCommandHandler = createGameCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveGames(string query = null)
    {
        return Ok(await _getActiveGamesQueryHandler.Handle(new GetActiveGamesQuery
        {
            Query = query
        }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameCommand command)
    {
        return Ok(await _createGameCommandHandler.Handle(command));
    }
}