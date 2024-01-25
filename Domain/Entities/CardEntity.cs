using Domain.Entities.Base;
using Domain.Entities.Identity;
using Domain.Entities.Transactions;

namespace Domain.Entities;

public class CardEntity : IEntity
{
    public Guid Id { get; set; }
    public double Balance { get; set; }
    
    public Guid OwnerId { get; set; }
    public UserEntity? Owner { get; set; }
    
    public int Points { get; set; }
    public DateTime PointsUpdatedAt { get; set; }
    
    public IEnumerable<TransactionEntity>? Transactions { get; set; }
}