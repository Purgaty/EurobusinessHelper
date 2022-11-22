using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Games.Queries.GetGameAccounts;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EurobusinessHelper.UI.ASP.Filters;

internal class GameManagementAuthFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var game = await GetGame(context);
        var identityId = await GetCurrentIdentityId(context);
        if (game.CreatedBy.Id != identityId)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Account can't manage game {game.Id}");
        if(game.State != GameState.Started)
            throw new EurobusinessException(EurobusinessExceptionCode.GameAccessDenied,
                $"Can't manage game in {game.State} state");
        
        await next.Invoke();
    }

    private static async Task<GetGameDetailsQueryResult> GetGame(ActionExecutingContext context)
    {
        var gameId = GetGameId(context);
        var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
        var game = await mediator.Send(new GetGameDetailsQuery
        {
            GameId = gameId
        });
        return game;
    }

    private static async Task<Guid> GetCurrentIdentityId(ActionExecutingContext context)
    {
        var securityContext = context.HttpContext.RequestServices.GetRequiredService<ISecurityContext>();
        var identity = await securityContext.GetCurrentIdentity();
        
        return identity.Id;
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