namespace EurobusinessHelper.Application.Identities.Command.CreateIdentity;

public class CreateIdentityCommand
{
    public CreateIdentityCommand(string email, string? name = default)
    {
        Email = email;
        Name = name;
    }
    public string Email { get; }
    public string? Name { get; }
}