using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Commands.CreateGameAccount;
using EurobusinessHelper.Application.Games.Commands.DeleteGame;
using EurobusinessHelper.Application.Games.Queries.GetActiveGames;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.UI.ASP.RequestModels.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveGames(string query = null)
    {
        return Ok(await _mediator.Send(new GetActiveGamesQuery
        {
            Query = query
        }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody]CreateGameCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("{gameId:guid}")]
    public async Task<IActionResult> GetGameDetails(Guid gameId)
    {
        var query = new GetGameDetailsQuery
        {
            GameId = gameId
        };
        return Ok(await _mediator.Send(query));
    }

    [HttpPut("{gameId:guid}")]
    public async Task<IActionResult> JoinGame(Guid gameId, [FromBody]JoinGameRequest request)
    {
        var command = new JoinGameCommand
        {
            GameId = gameId,
            Password = request.Password
        };
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{gameId:guid}")]
    public async Task<IActionResult> DeleteGame(Guid gameId)
    {
        var command = new DeleteGameCommand
        {
            GameId = gameId
        };
        await _mediator.Send(command);
        return NoContent();
    }
}