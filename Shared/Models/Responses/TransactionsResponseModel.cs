using Shared.Models.Entities;

namespace Shared.Models.Responses;

public class TransactionsResponseModel
{
    public IEnumerable<TransactionDetails>? Items { get; set; }
}