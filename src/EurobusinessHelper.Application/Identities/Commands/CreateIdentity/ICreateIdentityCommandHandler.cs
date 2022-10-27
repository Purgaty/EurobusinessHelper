using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Commands.CreateIdentity;

public interface ICreateIdentityCommandHandler : ICommandHandler<CreateIdentityCommand, Guid>
{
}