using EurobusinessHelper.Application.Common.Interfaces;

namespace EurobusinessHelper.Application.Identities.Command.CreateIdentity;

public interface ICreateIdentityCommandHandler : ICommandHandler<CreateIdentityCommand, Guid>
{
}