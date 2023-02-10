using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetActiveGames;

public class GetActiveGamesQuery : IRequest<GetActiveGamesQueryResult>
{
    public string Query { get; init; }
    public bool Joinable { get; set; }
    public Identity Participant { get; init; }
}