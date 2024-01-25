using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.UserManager;

public interface IBaseUserManager
{
    Task<IdentityResult> CreateAsync(UserEntity user, string password);
    Task<UserEntity?> FindByIdAsync(string userId);
    Task<UserEntity?> FindByEmailAsync(string userId);
    Task<bool> CheckPasswordAsync(UserEntity user, string password);
}