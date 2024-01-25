namespace Shared.Models.Requests;

public record CreateAccountRequestModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}