using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Mappings
{
    public static partial class IdentityMapper
    {
        public static IdentityDto AdaptToDto(this Identity p1)
        {
            return p1 == null ? null : new IdentityDto(p1.Email, p1.Name);
        }
        public static IdentityDto AdaptTo(this Identity p2, IdentityDto p3)
        {
            return p2 == null ? null : new IdentityDto(p2.Email, p2.Name);
        }
    }
}