using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace EurobusinessHelper.UI.ASP.Auth;

/// <summary>
/// Auth type constants
/// </summary>
public static class AuthTypeConsts
{
    /// <summary>
    /// List of authentication schemes
    /// </summary>
    public static readonly IReadOnlyDictionary<AuthType, string> AuthenticationSchemes
        = new Dictionary<AuthType, string>
        {
            [AuthType.Google] = GoogleDefaults.AuthenticationScheme,
            [AuthType.Microsoft] = MicrosoftAccountDefaults.AuthenticationScheme,
            [AuthType.Facebook] = FacebookDefaults.AuthenticationScheme
        };
}