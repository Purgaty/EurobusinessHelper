namespace EurobusinessHelper.Application.Common.Exceptions;

public class UnauthorizedException: EurobusinessException
{
    public UnauthorizedException() : base(EurobusinessExceptionCode.UnauthorizedUser, "User is not authorized")
    {
    }
}