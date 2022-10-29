using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.CreateGameAccount;

public class CreateGameAccountCommand : IRequest
{
    public Guid GameId { get; set; }
    public string Password { get; set; }
}