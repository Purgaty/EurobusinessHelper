using MediatR;

namespace EurobusinessHelper.Application.TransferRequest.Commands.ApproveRequest;

public class ApproveRequestCommand : IRequest
{
    public Guid RequestId { get; set; }
}