namespace EurobusinessHelper.Application.Games.Queries.GetActiveGamesQuery;

public class GetActiveGamesQueryResult
{
    public IEnumerable<Item> Items { get; init; }

    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}