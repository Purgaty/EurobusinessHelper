namespace EurobusinessHelper.Application.Common.Exceptions;

public enum EurobusinessExceptionCode
{
    InternalAppError,
    UnauthorizedUser,
    GameNotFound,
    AccountAlreadyExists,
    InvalidGamePassword,
    PasswordNotProvided,
    GameAccessDenied,
    InvalidGameStateChange,
    CannotJoinNotNewGame,
    StartingAccountBalanceNotProvided,
    AccountNotFound,
    InsufficientFunds
}