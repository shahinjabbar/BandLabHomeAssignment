namespace BandLabHomeAssigment.API.Responses;

public class PostModel
{
    public Guid Id { get; set; }
    public string Caption { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public List<CommentModel> Comments { get; set; } = [];

    public string ToCursor()
    {
        return $"{Comments.Count}_{CreatedAt}";
    }
}
