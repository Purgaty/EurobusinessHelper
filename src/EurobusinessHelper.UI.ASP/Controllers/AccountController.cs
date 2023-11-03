using EurobusinessHelper.Application.Accounts.Commands.RequestMoneyTransfer;
using EurobusinessHelper.Application.Accounts.Commands.TransferMoney;
using EurobusinessHelper.UI.ASP.Filters;
using EurobusinessHelper.UI.ASP.RequestModels.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
/// Account controller
/// </summary>
[Route("api/game/{gameId:guid}/account/{accountId:guid}")]
[GameAccountAuthFilter]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Transfer money
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="accountId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoney(Guid gameId, Guid accountId, [FromBody]TransferMoneyRequest request)
    {
        var command = new TransferMoneyCommand
        {
            FromAccount = accountId,
            GameId = gameId,
            ToAccount = request.AccountId,
            Amount = request.Amount
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestMoneyTransfer(Guid gameId, Guid accountId,
        [FromBody] RequestMoneyTransferRequest request)
    {
        var command = new RequestMoneyTransferCommand
        {
            GameId = gameId,
            FromAccount = accountId,
            ToAccount = request.AccountId,
            Amount = request.Amount
        };
        await _mediator.Send(command);
        return NoContent();
    }
}