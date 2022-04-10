using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;

public interface IGetIdentityByEmailQueryHandler : IQueryHandler<GetIdentityByEmailQuery, Domain.Entities.Identity?>
{
}