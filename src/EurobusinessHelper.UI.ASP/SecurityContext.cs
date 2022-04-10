using System.Linq;
using System.Threading.Tasks;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Application.Identities.Command.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Application.Mappings;
using EurobusinessHelper.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EurobusinessHelper.UI.ASP;

public class SecurityContext : ISecurityContext
{
    private const string NameClaimType = "name";
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGetIdentityByEmailQueryHandler _getIdentityByEmailQueryHandler;
    private readonly ICreateIdentityCommandHandler _createIdentityCommandHandler;
    private readonly IGetIdentityByIdQueryHandler _getIdentityByIdQueryHandler;
    private Identity? _currentIdentity;

    public SecurityContext(IHttpContextAccessor httpContextAccessor,
        IGetIdentityByEmailQueryHandler getIdentityByEmailQueryHandler,
        ICreateIdentityCommandHandler createIdentityCommandHandler,
        IGetIdentityByIdQueryHandler getIdentityByIdQueryHandler)
    {
        _httpContextAccessor = httpContextAccessor;
        _getIdentityByEmailQueryHandler = getIdentityByEmailQueryHandler;
        _createIdentityCommandHandler = createIdentityCommandHandler;
        _getIdentityByIdQueryHandler = getIdentityByIdQueryHandler;
    }

    public async Task<IdentityDto> GetCurrentIdentityDisplay()
    {
        return (await GetCurrentIdentity()).AdaptToDto();
    }

    public async Task<Identity> GetCurrentIdentity()
    {
        return _currentIdentity ??= await GetOrCreateIdentity();
    }

    private async Task<Identity> GetOrCreateIdentity()
    {
        var identity = await GetIdentity();
        if (identity != default)
            return identity;
        return await CreateIdentity();
    }

    private async Task<Identity> CreateIdentity()
    {
        var command = BuildCreateIdentityCommand();
        var identityId = await _createIdentityCommandHandler.Handle(command);
        var identity = await _getIdentityByIdQueryHandler.Handle(new GetIdentityByIdQuery(identityId));
        if (identity == default)
            throw new UnauthorizedException();
        return identity;
    }

    private CreateIdentityCommand BuildCreateIdentityCommand()
    {
        var email = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        var name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == NameClaimType)?.Value;
        if (email == default)
            throw new UnauthorizedException();
        return new CreateIdentityCommand(email, name);
    }

    private async Task<Identity?> GetIdentity()
    {
        var query = BuildGetIdentityByEmailQuery();
        return await _getIdentityByEmailQueryHandler.Handle(query);
    }

    private GetIdentityByEmailQuery BuildGetIdentityByEmailQuery()
    {
        var email = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (email == default)
            throw new UnauthorizedException();
        return new GetIdentityByEmailQuery(email);
    }
}