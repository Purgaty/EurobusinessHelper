using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Identities.Security;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EurobusinessHelper.UI.ASP.Filters;

internal class GameAccountAuthFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var gameId = GetGameId(context);
        var securityContext = context.HttpContext.RequestServices.GetRequiredService<ISecurityContext>();
        var games = await securityContext.GetCurrentIdentityGames();
        if (!games.Contains(gameId))
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Account can't access game {gameId}");
        
        await next.Invoke();
    }

    private static Guid GetGameId(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("gameId", out var gameIdArgument))
            throw new EurobusinessException(EurobusinessExceptionCode.InternalAppError,
                $"Argument gameId not found in action {context.ActionDescriptor.DisplayName}");
        if(gameIdArgument is not Guid gameId)
            throw new EurobusinessException(EurobusinessExceptionCode.InternalAppError,
                $"Value of argument gameId ({gameIdArgument}) is not a valid guid");

        return gameId;
    }
}