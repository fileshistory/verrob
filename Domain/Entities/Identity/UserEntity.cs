using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    public List<CardEntity>? Cards { get; set; }
}