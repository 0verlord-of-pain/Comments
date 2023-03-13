using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comments.API.Controllers;

[AllowAnonymous]
public class HealthCheckController : Controller
{
    [HttpGet("api/ping")]
    [AllowAnonymous]
    public IActionResult Ping()
    {
        return Ok();
    }
}