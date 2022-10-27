using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

[Route("/api/identity")]
public class IdentityController : ControllerBase
{
    private readonly ISecurityContext _securityContext;
    private readonly ILogger<IdentityController> _logger;

    public IdentityController(ISecurityContext securityContext, ILogger<IdentityController> logger)
    {
        _securityContext = securityContext;
        _logger = logger;
    }

    [HttpGet("current")]
    public async Task<ActionResult<Identity>> GetCurrentIdentity()
    {
        _logger.LogInformation("GetCurrentIdentity");
        return Ok(await _securityContext.GetCurrentIdentityDisplay());
    }
}