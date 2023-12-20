using BandLabHomeAssigment.API.Dtos.Posts;
using BandLabHomeAssigment.Domain.Validations;
using FluentValidation;

namespace BandLabHomeAssigment.API.Validators;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(ValidationOptions.CommentContentMaxLength);
    }
}
