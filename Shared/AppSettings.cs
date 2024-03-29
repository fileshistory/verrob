using Microsoft.IdentityModel.Tokens;

namespace Shared;

public abstract class AppSettings
{
    public abstract class Card
    {
        public const double Limit = 1500;
    }
    
    public abstract class Auth
    {
        public const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512Signature;
    }

    public abstract class ResponseOptions
    {
        public abstract class Transactions
        {
            public const int Limit = 10;
        }
    } 
}