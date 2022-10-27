namespace EurobusinessHelper.Application.Identities.Security;

public class IdentityDisplay
{
    public IdentityDisplay(string email, string name = default)
    {
        Email = email;
        Name = name;
    }

    public string Email { get; }
    public string Name { get; }
}