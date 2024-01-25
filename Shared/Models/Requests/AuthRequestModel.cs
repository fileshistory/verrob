namespace Shared.Models.Requests;

public record AuthRequestModel
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}