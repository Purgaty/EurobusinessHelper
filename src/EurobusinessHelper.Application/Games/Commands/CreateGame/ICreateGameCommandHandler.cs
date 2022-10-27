using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Games.Commands.CreateGame;

public interface ICreateGameCommandHandler : ICommandHandler<CreateGameCommand, Guid>
{
}