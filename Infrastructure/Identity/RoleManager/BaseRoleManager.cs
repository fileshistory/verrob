using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity.RoleManager;

public class BaseRoleManager : RoleManager<RoleEntity>, IBaseRoleManager
{
    public BaseRoleManager(
        IRoleStore<RoleEntity> store,
        IEnumerable<IRoleValidator<RoleEntity>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<RoleEntity>> logger
    ) : base(
        store,
        roleValidators,
        keyNormalizer,
        errors,
        logger
    )
    {
    }
}