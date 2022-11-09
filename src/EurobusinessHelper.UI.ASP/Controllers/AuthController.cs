﻿using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.UI.ASP.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EurobusinessHelper.UI.ASP.Controllers;

[Route("/api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AppConfig _appConfig;

    public AuthController(IOptions<AppConfig> appConfig)
    {
        _appConfig = appConfig.Value;
    }
    
    [HttpGet("challenge/{provider}")]
    public Task Challenge(AuthType provider, string redirectUri = "/")
    {
        return HttpContext.ChallengeAsync(AuthTypeConsts.AuthenticationSchemes[provider],
            new AuthenticationProperties
            {
                RedirectUri = redirectUri
            });
    }

    [HttpGet("authenticate")]
    public async Task<IActionResult> Authenticate()
    {
        await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/api/identity/current");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/api/identity/current");
    }

    [HttpGet("providers")]
    public IActionResult GetActiveProviders()
        => Ok(_appConfig.ActiveAuthenticationTypes);
}