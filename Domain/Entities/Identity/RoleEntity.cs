using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class RoleEntity : IdentityRole<Guid>, IEntity
{
    
}