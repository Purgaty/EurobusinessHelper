using EurobusinessHelper.Application.Accounts.Queries.CheckAccountGameAccess;
using EurobusinessHelper.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EurobusinessHelper.UI.ASP.Filters;

internal class GameAccountAuthFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var gameId = GetId(context, "gameId");
        var accountId = GetId(context, "accountId");
        var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

        var hasAccess = await mediator.Send(new CheckAccountGameAccessQuery
        {
            AccountId = accountId,
            GameId = gameId
        });
        
        if (!hasAccess)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Current identity can't access account {accountId} from game {gameId}");
        
        await next.Invoke();
    }

    private static Guid GetId(ActionExecutingContext context, string parameterName)
    {
        if (!context.ActionArguments.TryGetValue(parameterName, out var idArgument))
            throw new EurobusinessException(EurobusinessExceptionCode.InternalAppError,
                $"Argument {parameterName} not found in action {context.ActionDescriptor.DisplayName}");
        if(idArgument is not Guid guid)
            throw new EurobusinessException(EurobusinessExceptionCode.InternalAppError,
                $"Value of argument {parameterName} ({idArgument}) is not a valid guid");

        return guid;
    }
}