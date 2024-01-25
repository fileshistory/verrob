using Shared.Models.Entities;

namespace Shared.Models.Responses;

public record TransactionsResponseModel
{
    public IEnumerable<TransactionDetails>? Items { get; set; }
}