using System.Security.Claims;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Identities.Commands.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MapsterMapper;

namespace EurobusinessHelper.UI.ASP;

public class SecurityContext : ISecurityContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGetIdentityByEmailQueryHandler _getIdentityByEmailQueryHandler;
    private readonly ICreateIdentityCommandHandler _createIdentityCommandHandler;
    private readonly IGetIdentityByIdQueryHandler _getIdentityByIdQueryHandler;
    private readonly IMapper _mapper;
    private Identity _currentIdentity;

    public SecurityContext(IHttpContextAccessor httpContextAccessor,
        IGetIdentityByEmailQueryHandler getIdentityByEmailQueryHandler,
        ICreateIdentityCommandHandler createIdentityCommandHandler,
        IGetIdentityByIdQueryHandler getIdentityByIdQueryHandler,
        IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _getIdentityByEmailQueryHandler = getIdentityByEmailQueryHandler;
        _createIdentityCommandHandler = createIdentityCommandHandler;
        _getIdentityByIdQueryHandler = getIdentityByIdQueryHandler;
        _mapper = mapper;
    }

    public async Task<Identity> GetCurrentIdentity()
    {
        return _currentIdentity ??= await GetOrCreateIdentity();
    }

    public async Task<IdentityDisplay> GetCurrentIdentityDisplay()
    {
        var identity = await GetCurrentIdentity();
        return _mapper.Map<IdentityDisplay>(identity);
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
        var email = GetClaimValue(ClaimTypes.Email);
        if (email == default)
            throw new UnauthorizedException();
        var firstName = GetClaimValue(ClaimTypes.GivenName);
        var lastName = GetClaimValue(ClaimTypes.Surname);
        
        return new CreateIdentityCommand(email, firstName, lastName);
    }

    private async Task<Identity> GetIdentity()
    {
        var query = BuildGetIdentityByEmailQuery();
        return await _getIdentityByEmailQueryHandler.Handle(query);
    }

    private GetIdentityByEmailQuery BuildGetIdentityByEmailQuery()
    {
        var email = GetClaimValue(ClaimTypes.Email);
        if (email == default)
            throw new UnauthorizedException();
        return new GetIdentityByEmailQuery(email);
    }

    private string GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}