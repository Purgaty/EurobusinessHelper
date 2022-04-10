namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityById;

public class GetIdentityByIdQuery
{
    public GetIdentityByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}