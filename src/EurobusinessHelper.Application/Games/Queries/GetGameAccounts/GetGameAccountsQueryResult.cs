namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameAccountsQueryResult
{
    public ICollection<Item> Accounts { get; set; }
    public int Count => Accounts.Count;
    
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
    }
}