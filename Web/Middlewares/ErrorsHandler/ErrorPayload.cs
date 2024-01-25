using System.Text.Json;

namespace Web.Middlewares.ErrorsHandler;

public class ErrorPayload
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
