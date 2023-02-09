using EurobusinessHelper.Application.Games.Commands.CreateGame;
using EurobusinessHelper.Application.Games.Commands.CreateGameAccount;
using EurobusinessHelper.Application.Games.Commands.DeleteGame;
using EurobusinessHelper.Application.Games.Commands.UpdateGameState;
using EurobusinessHelper.Application.Games.Queries.GetActiveGames;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using EurobusinessHelper.UI.ASP.RequestModels.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
///     Game controller
/// </summary>
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISecurityContext _securityContext;

    /// <inheritdoc />
    public GameController(IMediator mediator, ISecurityContext securityContext)
    {
        _mediator = mediator;
        _securityContext = securityContext;
    }

    /// <summary>
    ///     Get active new games
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetActiveGames(string query = null)
    {
        return Ok(await _mediator.Send(new GetActiveGamesQuery
        {
            Query = query,
            States = new[] {GameState.New}
        }));
    }

    /// <summary>
    ///     Get games which the current account has joined
    /// </summary>
    /// <returns></returns>
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyGames(string query = null)
    {
        return Ok(await _mediator.Send(new GetActiveGamesQuery
        {
            Query = query,
            Owner = await _securityContext.GetCurrentIdentity()
        }));
    }

    /// <summary>
    ///     Create a new game
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    ///     Get game details
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpGet("{gameId:guid}")]
    public async Task<IActionResult> GetGameDetails(Guid gameId)
    {
        var query = new GetGameDetailsQuery
        {
            GameId = gameId
        };
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    ///     Join game
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{gameId:guid}")]
    public async Task<IActionResult> JoinGame(Guid gameId, [FromBody] JoinGameRequest request)
    {
        var command = new JoinGameCommand
        {
            GameId = gameId,
            Password = request.Password
        };
        await _mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Delete game
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Update game state
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    [HttpPut("{gameId:guid}/state/{state}")]
    public async Task<IActionResult> UpdateGameState(Guid gameId, GameState state)
    {
        var command = new UpdateGameStateCommand
        {
            GameId = gameId,
            State = state
        };
        await _mediator.Send(command);
        return NoContent();
    }
}