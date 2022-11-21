using MediatR;

namespace EurobusinessHelper.Application.Games.Queries.GetIdentityGames;

public class GetIdentityGamesQuery : IRequest<GetIdentityGamesQueryResult>
{
    public Guid IdentityId { get; set; }
}