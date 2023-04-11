using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models;
using WebApi.Services.LogicInterfaces;

namespace WebApi.Controllers; 

[ApiController]
[Route("/api/[controller]")]
[Authorize] //This means this Controller can only be interacted with, if the caller provides a valid JWT.
public class PostController : ControllerBase {

    private IPostLogic postLogic;

    public PostController(IPostLogic postLogic) {
        this.postLogic = postLogic;
    }

    [HttpPost ("createPost")]
    public async Task<ActionResult<ForumPost>> CreateAsync([FromBody] ForumPostDto postDto) {
        try {
            ForumPost post = await postLogic.CreatePostAsync(postDto);
            return Created($"/api/post/{post.PostId}", post);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ForumPost>> GetByIdAsync([FromRoute] int id) {
        try {
            ForumPost post = await postLogic.GetPostByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ForumPost>>> GetAllPostsAsync() {
    
        try {
            List<ForumPost> posts = await postLogic.GetAllPostsAsync();
            return Ok(posts);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut ("update")]
    public async Task<ActionResult> Update([FromBody] ForumPostUpdateDto dto) {
        try {
            await postLogic.UpdatePostAsync(dto);
            return Ok();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete ("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id) {
        try {
            await postLogic.DeletePostByIdAsync(id);
            return Ok();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}