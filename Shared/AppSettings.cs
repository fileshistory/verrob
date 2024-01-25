using Microsoft.IdentityModel.Tokens;

namespace Shared;

public abstract class AppSettings
{
    public abstract class Auth
    {
        public const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512Signature;
    }
}