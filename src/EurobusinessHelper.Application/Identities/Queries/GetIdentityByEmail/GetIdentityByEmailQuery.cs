using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;

public class GetIdentityByEmailQuery : IRequest<Identity>
{
    public GetIdentityByEmailQuery(string email)
    {
        Email = email;
    }

    public string Email { get; }
}