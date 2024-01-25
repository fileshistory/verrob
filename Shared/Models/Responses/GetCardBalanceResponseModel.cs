namespace Shared.Models.Responses;

public class GetCardBalanceResponseModel
{
    public double Balance { get; init; }
    public string? BalanceFormatted { get; init; }
    public double Available { get; init; }
    public string? AvailableFormated { get; init; }
}
