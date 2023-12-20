using BandLabHomeAssigment.API.Dtos.Posts;
using Microsoft.AspNetCore.Mvc;
using BandLabHomeAssigment.Application.Commands.CreatePost;
using Azure.Core;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using BandLabHomeAssigment.API.Queries;
using BandLabHomeAssigment.Domain;
using BandLabHomeAssigment.API.Responses;

namespace BandLabHomeAssigment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] ICreatePost createPost, [FromForm] CreatePostDto createPostDto)
    {
        Request.Headers.TryGetValue("X-UserId", out var userId);
        if (userId.IsNullOrEmpty())
        {
            return BadRequest("Missing header : X-UserId");
        }
       
        var command = new CreatePostCommand(createPostDto.Image, createPostDto.Caption, createPostDto.Comments, Guid.Parse(userId.ToString()));

        var postId = await createPost.ExecuteAsync(command);
        return CreatedAtAction(nameof(Create), postId);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromServices] IPostQueries contactQueries, [FromQuery] string? cursor = null, [FromQuery] int limit = 10)
    {
        Request.Headers.TryGetValue("X-UserId", out var userId);
        if (userId.IsNullOrEmpty())
        {
            return BadRequest("Missing header : X-UserId");
        }

        var posts = await contactQueries.GetPostsSortedByComments(cursor, limit, Guid.Parse(userId.ToString()));
        var nextCursor = posts.LastOrDefault().ToCursor();

        return Ok(new { Posts = posts, NextCursor = nextCursor });
    }

    //[HttpDelete("{postId}/comments/{commentId}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteComment([FromServices] IDeleteComment deleteComment, Guid postId, Guid commentId)
    //{
    //    Request.Headers.TryGetValue("X-UserId", out var userId);
    //    if (userId.IsNullOrEmpty())
    //    {
    //        return BadRequest("Missing header : X-UserId");
    //    }

    //    await deleteComment.ExecuteAsync(userId, postId, commentId);

    //    return NoContent();
    //}
}
