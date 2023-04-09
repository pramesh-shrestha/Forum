using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 

[ApiController]
[Route("/api/[controller]")]
[Authorize] //This means this Controller can only be interacted with, if the caller provides a valid JWT.
public class PostController : ControllerBase{
    
}