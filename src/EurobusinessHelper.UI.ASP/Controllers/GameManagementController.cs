using EurobusinessHelper.UI.ASP.Filters;
using EurobusinessHelper.UI.ASP.RequestModels.GameManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
/// Game controller
/// </summary>
[GameManagementAuthFilter]
[Route("api/gameManagement/{gameId:guid}")]
public class GameManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public GameManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Transfer money from bank to an account
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("transfer")]
    public async Task<IActionResult> TransferMoney(Guid gameId, TransferMoneyRequest request)
    {
        return NoContent();
    }
}