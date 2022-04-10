namespace EurobusinessHelper.Application.Identities.Security;

public class IdentityDto
{
    public IdentityDto(string email, string? name = default)
    {
        Email = email;
        Name = name;
    }

    public string Email { get; }
    public string? Name { get; }
}