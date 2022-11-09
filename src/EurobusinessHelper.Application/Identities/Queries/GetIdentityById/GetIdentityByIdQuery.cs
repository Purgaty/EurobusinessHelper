using EurobusinessHelper.Domain.Entities;
using MediatR;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityById;

public class GetIdentityByIdQuery : IRequest<Identity>
{
    public GetIdentityByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}