namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;

public class GetIdentityByEmailQuery
{
    public GetIdentityByEmailQuery(string email)
    {
        Email = email;
    }

    public string Email { get; }
}