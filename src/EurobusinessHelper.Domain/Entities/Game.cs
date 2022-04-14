namespace EurobusinessHelper.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Identity? CreatedBy { get; set; } = default;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}