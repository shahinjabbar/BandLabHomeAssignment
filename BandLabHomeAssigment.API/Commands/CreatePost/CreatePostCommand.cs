using BandLabHomeAssigment.API.Dtos.Posts;

namespace BandLabHomeAssigment.Application.Commands.CreatePost;

public record CreatePostCommand(IFormFile Image, string Caption, IEnumerable<CreateCommentDto> Comments, Guid CreatorId);
