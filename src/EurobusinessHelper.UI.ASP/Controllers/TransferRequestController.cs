using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
/// Identity controller
/// </summary>
[Route("/api/transferRequest")]
public class TransferRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransferRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPut("{requestId:guid}/approve")]
    public async Task<IActionResult> ApproveRequest(Guid requestId)
    {

        return NoContent();
    }
}