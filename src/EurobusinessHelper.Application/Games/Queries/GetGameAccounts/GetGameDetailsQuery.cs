using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameDetailsQuery : IRequest<GetGameDetailsQueryResult>
{
    public Guid GameId { get; init; }
}