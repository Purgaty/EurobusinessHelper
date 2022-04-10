﻿using System.Threading.Tasks;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EurobusinessHelper.UI.ASP.Controllers;

[Route("/api/identity")]
public class IdentityController : ControllerBase
{
    private readonly ISecurityContext _securityContext;

    public IdentityController(ISecurityContext securityContext)
    {
        _securityContext = securityContext;
    }

    [HttpGet("current")]
    public async Task<ActionResult<Identity>> GetCurrentIdentity()
    {
        return Ok(await _securityContext.GetCurrentIdentityDisplay());
    }

    [HttpGet("dummyAnonymous")]
    [AllowAnonymous]
    public IActionResult DummyAnonymous()
    {
        return Ok("you're in");
    }
}