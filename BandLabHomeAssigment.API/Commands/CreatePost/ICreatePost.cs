namespace BandLabHomeAssigment.Application.Commands.CreatePost;

public interface ICreatePost
{
    Task<Guid> ExecuteAsync(CreatePostCommand command);
}
