using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Identities.Security;

public interface ISecurityContext
{
    public Task<Identity> GetCurrentIdentity();
    Task<IdentityDisplay> GetCurrentIdentityDisplay();
    Task<IEnumerable<Guid>> GetCurrentIdentityGames();
}