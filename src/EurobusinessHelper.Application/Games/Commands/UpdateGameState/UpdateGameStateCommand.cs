using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.UpdateGameState;

public class UpdateGameStateCommand : IRequest
{
    public Guid GameId { get; set; }
    public GameState State { get; set; }
}