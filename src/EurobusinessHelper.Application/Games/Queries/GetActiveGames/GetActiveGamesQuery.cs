using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetActiveGames;

public class GetActiveGamesQuery : IRequest<GetActiveGamesQueryResult>
{
    public string Query { get; init; }
    public IEnumerable<GameState> States { get; init; } = Enumerable.Empty<GameState>();
    public Identity Participant { get; init; }
}