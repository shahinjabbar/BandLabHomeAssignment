using BandLabHomeAssigment.API.Responses;
using BandLabHomeAssigment.Domain.Repositories;
using BandLabHomeAssigment.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BandLabHomeAssigment.API.Queries;

public class PostQueries(ApplicationDbContext dbContext, IBlobStorageRepository blobStorageRepository) : IPostQueries
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;

    public async Task<IEnumerable<PostModel>> GetPostsSortedByComments(string cursor, int limit, Guid userId)
    {
        var query = _dbContext.Posts
            .Include(p => p.Comments)
            .Where(p => p.CreatorId == userId)
            .AsQueryable();

        if (!string.IsNullOrEmpty(cursor))
        {
            var (lastCommentCount, lastCreatedAt) = ParseCursor(cursor);

            if (lastCreatedAt.HasValue)
            {
                query = query
                    .Where(p => (p.Comments.Count > lastCommentCount) ||
                                 (p.Comments.Count == lastCommentCount && p.CreatedAt > lastCreatedAt.Value));
            }
            else
            {
                query = query
                   .Where(p => p.Comments.Count > lastCommentCount);
            }
        }

        query = query.OrderByDescending(p => p.Comments.Count);

        query = query.Take(limit);

        var posts = await query
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Caption= p.Caption,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    Comments = p.Comments
                        .OrderByDescending(c => c.CreatedAt)
                        .Take(2)
                        .Select(c => new CommentModel
                        {
                            Id = c.Id,
                            Content = c.Content
                        })
                        .ToList()
                })
                .ToListAsync();

        foreach (var post in posts)
        {
            var resizedImageUrl = await _blobStorageRepository.GetResizedImageUrlAsync(post.ImageUrl);
            post.ImageUrl = resizedImageUrl;
        }

        return posts;
    }

    private static (int lastCommentCount, DateTime? lastCreatedAt) ParseCursor(string cursor)
    {
        var parts = cursor.Split('_');
        if (parts.Length == 2 && int.TryParse(parts[0], out int lastCommentCount))
        {
            if (DateTime.TryParse(parts[1], out DateTime createdAt))
            {
                return (lastCommentCount, createdAt);
            }
        }
        return (0, null);
    }
}
