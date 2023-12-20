using BandLabHomeAssigment.Domain;
using BandLabHomeAssigment.Domain.Repositories;

namespace BandLabHomeAssigment.Application.Commands.CreatePost;

public class CreatePost(IPostRepository postRepository, IBlobStorageRepository blobStorageRepository) : ICreatePost
{
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;

    public async Task<Guid> ExecuteAsync(CreatePostCommand command)
    {
        var fileUrl = string.Empty;
        await using (var fileStream = command.Image.OpenReadStream())
        {
            fileUrl =  await _blobStorageRepository.UploadFileAsync(fileStream, command.Image.FileName);
        }

        var newPost = new Post(command.Caption, fileUrl, command.CreatorId);

        newPost.AddComments(command.Comments.Select(x => new Comment(x.Content, command.CreatorId, newPost.Id)));

        return await _postRepository.AddAsync(newPost);
    }
}