using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Common.Exceptions;

public class StateMachineException : EurobusinessException
{
    public StateMachineException(Guid gameId, GameState from, GameState to)
        : base(EurobusinessExceptionCode.InvalidGameStateChange, "Invalid state change")
    {
        GameId = gameId;
        From = from;
        To = to;
    }

    public Guid GameId { get; }
    public GameState From { get; }
    public GameState To { get; }
}