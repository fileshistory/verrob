using Domain.Entities.Base;

namespace Domain.Entities.Transactions;

public class TransactionEntity : IEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Sum { get; set; }
    public DateTime ProcessingDate { get; set; }
    public string? Picture { get; set; }
    
    public Guid TypeId { get; set; }
    public TransactionTypeEntity? Type { get; set; }
    
    public Guid StatusId { get; set; } 
    public TransactionStatusEntity? Status { get; set; }
    
    public Guid CardId { get; set; }
    public CardEntity? Card { get; set; }
}

