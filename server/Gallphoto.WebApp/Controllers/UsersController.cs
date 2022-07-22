using Gallphoto.WebApp.Core.Attributes;
using Gallphoto.WebApp.Models.Requests.Users;
using Gallphoto.WebApp.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Gallphoto.WebApp.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPut("SignUp")]
    public IActionResult SignUp(RegisterNewUserRequest model)
    {
         var (user, err) = _userService.RegisterNewUser(model);
         if (!string.IsNullOrEmpty(err))
         {
             return BadRequest(new { message = err });
         }
        
        return Ok();
    }
    
    [HttpPost("Authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });
        
        return Ok(response.Token);
    }

    [Authorize]
    [HttpGet]
    [Route("Details")]
    public IActionResult Details()
    {
        return Ok(HttpContext.Items["User"]);
    }
}