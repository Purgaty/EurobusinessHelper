using EurobusinessHelper.Application.TransferRequest.Commands.ApproveRequest;
using EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;
using EurobusinessHelper.UI.ASP.RequestModels.TransferRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
/// Transfer request controller
/// </summary>
[Route("/api/transferRequest")]
public class TransferRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransferRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest([FromBody]CreateTransferRequestRequest request)
    {
        var command = new CreateTransferRequestCommand
        {
            AccountId = request.AccountId,
            Amount = request.Amount
        };
        
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("{requestId:guid}/approve")]
    public async Task<IActionResult> ApproveRequest(Guid requestId)
    {
        await _mediator.Send(new ApproveRequestCommand {RequestId = requestId});
        return NoContent();
    }
}