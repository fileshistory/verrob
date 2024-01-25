using Domain.Entities.Base;

namespace Domain.Entities.Transactions;

public class TransactionStatusEntity : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}