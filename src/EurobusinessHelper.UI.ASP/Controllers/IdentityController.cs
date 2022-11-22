using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
/// Identity controller
/// </summary>
[Route("/api/identity")]
public class IdentityController : ControllerBase
{
    private readonly ISecurityContext _securityContext;

    /// <inheritdoc />
    public IdentityController(ISecurityContext securityContext)
    {
        _securityContext = securityContext;
    }

    /// <summary>
    /// Get current identity
    /// </summary>
    /// <returns></returns>
    [HttpGet("current")]
    public async Task<ActionResult<Identity>> GetCurrentIdentity()
    {
        return Ok(await _securityContext.GetCurrentIdentityDisplay());
    }
}