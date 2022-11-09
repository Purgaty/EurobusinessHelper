using System.Security.Cryptography;

namespace EurobusinessHelper.Application.Common.Utilities.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string GetPasswordHash(string password)
    {
        var generator = new Rfc2898DeriveBytes(password, 0);
        var hash = generator.GetBytes(20);
        return Convert.ToBase64String(hash);
    }

    public bool ValidatePassword(string passwordToCheck, string actualPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordToCheck) || string.IsNullOrWhiteSpace(actualPasswordHash))
            return false;
        var hashToCheck = GetPasswordHash(passwordToCheck);

        return actualPasswordHash.Equals(hashToCheck);
    }
    
}

public interface IPasswordHasher
{
    string GetPasswordHash(string password);
    bool ValidatePassword(string passwordToCheck, string actualPasswordHash);
}