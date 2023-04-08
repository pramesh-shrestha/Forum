
using Microsoft.AspNetCore.Mvc;

using Shared.Models;
using WebApi.Services.LogicInterfaces;

namespace WebApi.Controllers; 

[ApiController]
[Route("/api/[controller]")]

public class UserController : ControllerBase {

    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic) {
        this.userLogic = userLogic;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser([FromBody] User user) {
        try {
            User newUser = await userLogic.RegisterAsync(user);
            return Created($"/api/user/{user.Username}", newUser);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}