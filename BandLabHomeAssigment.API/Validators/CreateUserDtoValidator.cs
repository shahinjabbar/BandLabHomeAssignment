using BandLabHomeAssigment.API.Dtos.Users;
using BandLabHomeAssigment.Domain.Validations;
using FluentValidation;

namespace BandLabHomeAssigment.API.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationOptions.UserNameMaxLength);
    }
}
