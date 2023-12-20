namespace BandLabHomeAssigment.Domain;

public class Post(string caption, string imageUrl, Guid creatorId) : DomainEntity
{
    public string Caption { get; private set; } = caption;
    public string ImageUrl { get; private set; } = imageUrl;
    public Guid CreatorId { get; private set; } = creatorId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    private readonly List<Comment> _comments = [];
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    public void AddComments(IEnumerable<Comment> comments)
    {
        _comments.AddRange(comments);
    }
}
