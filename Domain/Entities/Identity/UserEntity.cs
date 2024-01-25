using Domain.Entities.Base;
using Domain.Entities.Transactions;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    public List<CardEntity>? Cards { get; set; }
}