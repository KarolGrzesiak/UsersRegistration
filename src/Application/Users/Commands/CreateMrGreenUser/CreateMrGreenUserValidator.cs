using FluentValidation;

namespace Application.Users.Commands.CreateMrGreenUser
{
    public class CreateMrGreenUserValidator : AbstractValidator<CreateMrGreenUser>
    {
        public CreateMrGreenUserValidator()
        {
            RuleFor(u=>u.FirstName)
                .MaximumLength(200).WithMessage($"{nameof(CreateMrGreenUser.FirstName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.FirstName)} is required");
            RuleFor(u=>u.LastName)
                .MaximumLength(200).WithMessage($"{nameof(CreateMrGreenUser.LastName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.LastName)} is required");
            RuleFor(u=>u.Street)
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.Street)} is required");
            RuleFor(u=>u.BuildingNumber)
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.BuildingNumber)} is required");
            RuleFor(u=>u.ZipCode)
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.ZipCode)} is required");
            RuleFor(u=>u.PersonalNumber)
                .NotEmpty().WithMessage($"{nameof(CreateMrGreenUser.PersonalNumber)} is required");

        }
    }
}