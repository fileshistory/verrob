using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class CardEntity : IEntity
{
    public Guid Id { get; set; }
    public double Balance { get; set; }
    
    public Guid OwnerId { get; set; }
    public UserEntity? Owner { get; set; }
}