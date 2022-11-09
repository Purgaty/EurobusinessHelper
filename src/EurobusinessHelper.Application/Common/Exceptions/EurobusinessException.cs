namespace EurobusinessHelper.Application.Common.Exceptions;

public class EurobusinessException : Exception
{
    public EurobusinessException(EurobusinessExceptionCode code, string message)
        : base(message)
    {
        Code = code;
    }

    public EurobusinessException(EurobusinessExceptionCode code, string message, Exception exception)
        : base(message, exception)
    {
        Code = code;
    }

    public EurobusinessExceptionCode Code { get; init; }
}