using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameAccountsQuery : IRequest<GetGameAccountsQueryResult>
{
    public Guid GameId { get; set; }
}