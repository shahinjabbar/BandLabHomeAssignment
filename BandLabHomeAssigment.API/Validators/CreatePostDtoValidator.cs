using BandLabHomeAssigment.API.Dtos.Posts;
using BandLabHomeAssigment.Domain.Validations;
using FluentValidation;

namespace BandLabHomeAssigment.API.Validators;

public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(x => x.Image)
            .Must(x => x != null)
            .Must(BeAValidSize).WithErrorCode($"Image must be less than 100MB")
            .Must(BeAValidFormat).WithMessage($"Only {string.Join(",", ValidationOptions.SupportedExtensions)} formats are allowed");

        RuleFor(x => x.Caption)
                 .NotEmpty()
                 .MaximumLength(ValidationOptions.PostCaptionMaxLength);

        RuleForEach(x => x.Comments).SetValidator(new CreateCommentDtoValidator());
    }

    private bool BeAValidSize(IFormFile file)
    {
        return file.Length <= ValidationOptions.ImageSizeLimit;
    }

    private bool BeAValidFormat(IFormFile file)
    {
        return ValidationOptions.SupportedExtensions.Any(ext => file.FileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }
}
