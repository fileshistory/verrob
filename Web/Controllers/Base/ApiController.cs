using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Base;

[ApiController, Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private string? AuthorizedUserId => User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

    protected Guid GetAuthorizedUsedId()
    {
        if (string.IsNullOrEmpty(AuthorizedUserId))
        {
            throw new Exception("Unauthorized");
        }

        return Guid.Parse(AuthorizedUserId);
    }
}