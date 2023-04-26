
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using Shared.Models;

namespace WebApi.Controllers; 

[ApiController]
[Route("/api/[controller]")]

public class UserController : ControllerBase {

    private readonly IUserLogic userLogic;
    private readonly IConfiguration config;

    public UserController(IConfiguration config, IUserLogic userLogic) {
        this.config = config;
        this.userLogic = userLogic;
    }

    //Generating claims of User
    private List<Claim> GenerateClaims(User user) {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("DisplayName", user.Firstname + " " + user.Lastname)
        };
        return claims.ToList();
    }
    
    //Generating JWT 
    private string GenerateJwt(User user) {
        List<Claim> claims = GenerateClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        //creating header
        JwtHeader header = new JwtHeader(signIn);
        
        //creating payload
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60)
            );

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    

    //register
    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser([FromBody] User user) {
        try {
            Console.WriteLine("Controller");
            User newUser = await userLogic.RegisterAsync(user);
            return Created($"/api/user/{user.Username}", newUser);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //login
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto) {
        try {
            User user = await userLogic.ValidateUser(userLoginDto);
            string token = GenerateJwt(user);
            return Ok(token);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}