using MediatR;

namespace EurobusinessHelper.Application.Accounts.Queries.CheckAccountGameAccess;

public class CheckAccountGameAccessQuery : IRequest<bool>
{
    public Guid GameId { get; set; }
    public Guid AccountId { get; set; }
}