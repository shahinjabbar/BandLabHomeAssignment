namespace BandLabHomeAssigment.Domain.Validations;

public static class ValidationOptions
{
    public static readonly int UserNameMaxLength = 100;
    public static readonly int PostCaptionMaxLength = 255;
    public static readonly int CommentContentMaxLength = 255;
    public static readonly int ImageUrlMaxLength = 2048;
    public static readonly int ImageSizeLimit = 104857600; // 100MB
    public static IEnumerable<string> SupportedExtensions => new[] { "JPG", "PNG", "BMP" };
}