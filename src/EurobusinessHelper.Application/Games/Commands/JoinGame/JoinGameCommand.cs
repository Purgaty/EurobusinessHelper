using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.JoinGame;

public class JoinGameCommand : IRequest
{
    public Guid GameId { get; init; }
    public string Password { get; init; }
}