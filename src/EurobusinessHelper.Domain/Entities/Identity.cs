namespace EurobusinessHelper.Domain.Entities;

public class Identity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}