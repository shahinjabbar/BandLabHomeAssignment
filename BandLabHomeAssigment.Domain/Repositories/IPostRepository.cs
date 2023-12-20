namespace BandLabHomeAssigment.Domain.Repositories;

public interface IPostRepository
{
    Task<Guid> AddAsync(Post post);
}
