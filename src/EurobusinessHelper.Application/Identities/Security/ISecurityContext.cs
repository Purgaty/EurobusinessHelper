using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Identities.Security;

public interface ISecurityContext
{
    public Task<Identity> GetCurrentIdentity();
    Task<IdentityDto> GetCurrentIdentityDisplay();
}