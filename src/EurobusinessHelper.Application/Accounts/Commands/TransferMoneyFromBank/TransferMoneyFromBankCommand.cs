using MediatR;

namespace EurobusinessHelper.Application.Accounts.Commands.TransferMoneyFromBank;

public class TransferMoneyFromBankCommand : IRequest
{
    public Guid ToAccount { get; set; }
    public int Amount { get; set; }
}