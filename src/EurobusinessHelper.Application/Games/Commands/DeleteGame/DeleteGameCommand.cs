using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.DeleteGame;

public class DeleteGameCommand : IRequest
{
    public Guid GameId { get; init; }
}