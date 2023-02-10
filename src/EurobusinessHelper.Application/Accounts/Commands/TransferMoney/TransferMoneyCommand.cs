using MediatR;

namespace EurobusinessHelper.Application.Accounts.Commands.TransferMoney;

public class TransferMoneyCommand : IRequest
{
    public Guid GameId { get; set; }
    public Guid ToAccount { get; set; }
    public Guid FromAccount { get; set; }
    public int Amount { get; set; }
}