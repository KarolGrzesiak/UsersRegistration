using FluentValidation;

namespace Application.Users.Commands.UpdateMrGreenUser
{
    public class UpdateMrGreenUserValidator : AbstractValidator<UpdateMrGreenUser>
    {
        public UpdateMrGreenUserValidator()
        {
            RuleFor(u=>u.Id)
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.Id)} is required");
            RuleFor(u=>u.FirstName)
                .MaximumLength(200).WithMessage($"{nameof(UpdateMrGreenUser.FirstName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.FirstName)} is required");
            RuleFor(u=>u.LastName)
                .MaximumLength(200).WithMessage($"{nameof(UpdateMrGreenUser.LastName)} must not exceed 200 characters")
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.LastName)} is required");
            RuleFor(u=>u.Street)
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.Street)} is required");
            RuleFor(u=>u.BuildingNumber)
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.BuildingNumber)} is required");
            RuleFor(u=>u.ZipCode)
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.ZipCode)} is required");
            RuleFor(u=>u.PersonalNumber)
                .NotEmpty().WithMessage($"{nameof(UpdateMrGreenUser.PersonalNumber)} is required");

        }
    }
}