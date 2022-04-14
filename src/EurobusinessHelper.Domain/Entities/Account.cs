namespace EurobusinessHelper.Domain.Entities;
public class Account
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Identity Owner { get; set; } = default!;
    public Game Game { get; set; } = default!;
}