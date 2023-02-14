using MediatR;

namespace EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;

public class CreateTransferRequestCommand : IRequest<Guid>
{
    public Guid AccountId { get; set; }
    public int Amount { get; set; }
}