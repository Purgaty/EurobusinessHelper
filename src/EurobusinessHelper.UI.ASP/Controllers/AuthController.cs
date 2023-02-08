using System.Net;
using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.UI.ASP.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EurobusinessHelper.UI.ASP.Controllers;

/// <summary>
///     Auth controller
/// </summary>
[Route("/api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AppConfig _appConfig;

    /// <inheritdoc />
    public AuthController(IOptions<AppConfig> appConfig)
    {
        _appConfig = appConfig.Value;
    }

    /// <summary>
    ///     Redirect to identity provider
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    [HttpGet("challenge/{provider}")]
    public Task Challenge(AuthType provider, string redirectUri = "/")
    {
        return HttpContext.ChallengeAsync(AuthTypeConsts.AuthenticationSchemes[provider],
            new AuthenticationProperties
            {
                RedirectUri = redirectUri
            });
    }

    /// <summary>
    ///     Authentication
    /// </summary>
    /// <returns></returns>
    [HttpGet("authenticate")]
    public async Task<IActionResult> Authenticate()
    {
        await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/api/identity/current");
    }

    /// <summary>
    ///     Logout
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/api/identity/current");
    }

    /// <summary>
    ///     Get active identity providers
    /// </summary>
    /// <returns></returns>
    [HttpGet("providers")]
    public IActionResult GetActiveProviders()
    {
        return Ok(_appConfig.ActiveAuthenticationTypes);
    }
}