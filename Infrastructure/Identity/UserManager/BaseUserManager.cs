using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.UserManager;

public class BaseUserManager : UserManager<UserEntity>, IBaseUserManager
{
    public BaseUserManager(
        IUserStore<UserEntity> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<UserEntity> passwordHasher,
        IEnumerable<IUserValidator<UserEntity>> userValidators,
        IEnumerable<IPasswordValidator<UserEntity>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<UserEntity>> logger
    ) : base(
        store,
        optionsAccessor,
        passwordHasher,
        userValidators,
        passwordValidators,
        keyNormalizer,
        errors,
        services,
        logger
    )
    {
    }
}