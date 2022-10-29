using MediatR;

namespace EurobusinessHelper.Application.Games.Commands.CreateGame;

public class CreateGameCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public bool IsPasswordProtected { get; set; }
    public string Password { get; set; }
}