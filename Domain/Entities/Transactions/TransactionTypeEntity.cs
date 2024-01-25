using Domain.Entities.Base;

namespace Domain.Entities.Transactions;

public class TransactionTypeEntity : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}