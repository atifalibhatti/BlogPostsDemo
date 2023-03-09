using LetsPost.UserManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsPost.UserManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [Authorize, HttpGet("Validate")]
    public async Task<IActionResult> Validate()
    {
        return Ok(true);
    }
}
