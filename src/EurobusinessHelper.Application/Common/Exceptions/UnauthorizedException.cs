namespace EurobusinessHelper.Application.Common.Exceptions;

public class UnauthorizedException: ApplicationException
{
    public UnauthorizedException() : base("User is not authorized")
    {
    }
}