using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.UserManager;

public interface IBaseUserManager
{
    Task<IdentityResult> CreateAsync(UserEntity user, string password);
}