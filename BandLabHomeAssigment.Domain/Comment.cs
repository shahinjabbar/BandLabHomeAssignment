namespace BandLabHomeAssigment.Domain;

public class Comment(string content, Guid creatorId, Guid postId) : DomainEntity
{
    public string Content { get; private set; } = content;
    public Guid CreatorId { get; private set; } = creatorId;
    public Guid PostId { get; private set; } = postId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
