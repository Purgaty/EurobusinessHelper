using MediatR;

namespace EurobusinessHelper.Application.Accounts.Queries.GetAccountGame;

public class GetAccountGameQuery : IRequest<Guid>
{
    public Guid AccountId { get; set; }
}