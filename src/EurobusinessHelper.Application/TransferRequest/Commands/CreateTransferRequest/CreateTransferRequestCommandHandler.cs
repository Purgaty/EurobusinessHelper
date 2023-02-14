using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.TransferRequest.Commands.CreateTransferRequest;

public class CreateTransferRequestCommandHandler : IRequestHandler<CreateTransferRequestCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTransferRequestCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateTransferRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.TransferRequest
        {
            Account = new Account {Id = request.AccountId},
            Amount = request.Amount
        };

        _dbContext.TransferRequest.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}