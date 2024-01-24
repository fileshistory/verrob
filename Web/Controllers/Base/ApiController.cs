using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Base;

[ApiController, Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    
}