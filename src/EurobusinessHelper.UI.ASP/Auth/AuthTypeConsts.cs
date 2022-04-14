using EurobusinessHelper.UI.ASP.Auth;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace EurobusinessHelper.UI.Auth;

public static class AuthTypeConsts
{
    public static readonly IReadOnlyDictionary<AuthType, string> AuthenticationSchemes
        = new Dictionary<AuthType, string>
        {
            [AuthType.Google] = GoogleDefaults.AuthenticationScheme,
            [AuthType.Microsoft] = MicrosoftAccountDefaults.AuthenticationScheme,
            [AuthType.Facebook] = FacebookDefaults.AuthenticationScheme
        };
}