namespace EurobusinessHelper.Application.Identities.Commands.CreateIdentity;

public class CreateIdentityCommand
{
    public CreateIdentityCommand(string email, string? firstName = default, string? lastName = default)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    public string Email { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
}