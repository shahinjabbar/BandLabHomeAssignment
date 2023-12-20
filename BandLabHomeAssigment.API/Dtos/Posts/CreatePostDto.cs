namespace BandLabHomeAssigment.API.Dtos.Posts;

public record CreatePostDto(IFormFile Image, string Caption, IEnumerable<CreateCommentDto> Comments);

