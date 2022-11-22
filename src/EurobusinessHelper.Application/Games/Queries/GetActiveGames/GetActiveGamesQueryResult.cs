using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Games.Queries.GetActiveGames;

public class GetActiveGamesQueryResult
{
    public IEnumerable<Item> Items { get; init; }

    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public GameState State { get; init; }
        public bool IsPasswordProtected { get; init; }
    }
}