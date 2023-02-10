using System.Security.Claims;
using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Application.Identities.Commands.CreateIdentity;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityByEmail;
using EurobusinessHelper.Application.Identities.Queries.GetIdentityById;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace EurobusinessHelper.UI.ASP;

/// <summary>
/// Security context containing information about
/// currently authenticated identity
/// </summary>
public class SecurityContext : ISecurityContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private Identity _currentIdentity;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public SecurityContext(IHttpContextAccessor httpContextAccessor,
        IMediator mediator,
        IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets or creates identity for currently authenticated user
    /// </summary>
    /// <returns></returns>
    public async Task<Identity> GetCurrentIdentity()
    {
        return _currentIdentity ??= await GetOrCreateIdentity();
    }

    /// <summary>
    /// Gets the display object for current identity
    /// </summary>
    /// <returns></returns>
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
        var identityId = await _mediator.Send(command);
        var identity = await _mediator.Send(new GetIdentityByIdQuery(identityId));
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
        return await _mediator.Send(query);
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