using BandLabHomeAssigment.Domain;
using BandLabHomeAssigment.Domain.Repositories;

namespace BandLabHomeAssigment.Infrastructure.Repositories;

public class PostRepository(ApplicationDbContext context) : IPostRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post.Id;
    }
}
