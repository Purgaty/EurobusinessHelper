using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityById;

public interface IGetIdentityByIdQueryHandler : IQueryHandler<GetIdentityByIdQuery, Domain.Entities.Identity?>
{
}