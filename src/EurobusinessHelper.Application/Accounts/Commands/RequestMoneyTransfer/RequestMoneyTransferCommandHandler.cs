using EurobusinessHelper.Application.Common.Interfaces;
using MediatR;

namespace EurobusinessHelper.Application.Accounts.Commands.RequestMoneyTransfer;

public class RequestMoneyTransferCommandHandler : IRequestHandler<RequestMoneyTransferCommand>
{
    private readonly IGameHubConnector _gameHubConnector;

    public RequestMoneyTransferCommandHandler(IGameHubConnector gameHubConnector)
    {
        _gameHubConnector = gameHubConnector;
    }
    
    public async Task<Unit> Handle(RequestMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        await _gameHubConnector.RequestMoneyTransfer(request.GameId, request.FromAccount, request.ToAccount,
            request.Amount);

        return Unit.Value;
    }
}