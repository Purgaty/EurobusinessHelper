using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetActiveGames;

public class GetActiveGamesQuery : IRequest<GetActiveGamesQueryResult>
{
    public string Query { get; init; }
}